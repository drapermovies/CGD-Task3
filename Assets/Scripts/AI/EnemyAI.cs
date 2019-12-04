using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region Header 
    [Header("Default Values")]
    [SerializeField] private float walk_speed = 1.3f;
    [SerializeField] private float run_speed = 2.85f;
    public float jumpSpeed = 600.0f;
    public float EnemyRunDistance = 4.0f;

    [Header("Jump")]
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public Rigidbody rb;

    public bool has_bumped { get; set; }

    [Header("Bump")]
    [SerializeField] private float bump_range = 1.25f;

    [Header("Particles")]
    [SerializeField] private ParticleSystem run_particles = null;
    [SerializeField] private ParticleSystem bump_particles = null;

    private Animator anim;

    private NavMeshAgent[] nav_agents;

    private float vSpeed = 0.0f;
    private float new_bump = 0.0f;

    private bool has_died = false;

    private Transform tform;
    #endregion

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetBool("isIdle", true);
        tform = GetComponent<Transform>();

        //Loop through children to get the correct children we need
        foreach(Transform child in GetComponentsInChildren<Transform>())
        {
            if(child.name == "Running Particles" && !run_particles)
            {
                run_particles = child.GetComponent<ParticleSystem>();
            }
            else if(child.name == "Collision Particles" && !bump_particles)
            {
                bump_particles = child.GetComponent<ParticleSystem>();
            }
        }

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        nav_agents = FindObjectsOfType<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, 
                                       whatIsGround);
        vSpeed = rb.velocity.y;
        anim.SetFloat("vSpeed", vSpeed);
    }
    void Update()
    {
        if (!has_died)
        {
            CheckIdleState();

            FindPlayer();

            BumpCheck();

            if (anim.GetBool("isRun") && !run_particles.isPlaying)
            {
                run_particles.Play();
            }
        }
    }

    public void Jump()
    {
        if (grounded && rb.velocity.y == 0)
        {
            anim.SetTrigger("isJump");
            rb.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
        }
    }

    public void UpdateTargets(Vector3 target_pos)
    {
        foreach (NavMeshAgent agent in nav_agents)
        {
            agent.destination = target_pos;
            //Debug.Log("Destination: " + agent.destination);
        }
    }

    //Makes player idle when they reach their destination
    private void CheckIdleState()
    {
        foreach(NavMeshAgent agent in nav_agents)
        {
            if(Vector3.Distance(transform.position, agent.destination) <= bump_range)
            {
                Idle();
            }
        }
    }

    public void CrippledWalk()
    {
        anim.SetBool("crippled", true);
        anim.SetBool("isIdle", false);

        GetComponent<NavMeshAgent>().speed = walk_speed; //Move slower

        EnemyAudioManager.is_walking = true;
    }

    //Play Idle Animation and find new waypoint
    public void Idle()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isRun", false);
        anim.SetBool("crippled", false);
        anim.SetBool("dancing", false);

        //Debug.Log("Idle");

        run_particles.Clear();
        run_particles.Stop();

        FindNearestWaypoint(); //Go to nearest point if idle

        EnemyAudioManager.is_walking = false;
    }

    public void Run()
    {
        anim.SetBool("isRun", !(anim.GetBool("isRun")));
        anim.SetBool("isIdle", false);
        anim.SetBool("crippled", false);

        GetComponent<NavMeshAgent>().speed = run_speed; //Move faster

        EnemyAudioManager.is_walking = true;
    }

    //Plays a particle when we 'bump' into something
    public void Bump()
    {
        if (!has_bumped)
        {
            bump_particles.Play();
            has_bumped = true;
        }
    }

    //Randomly generates a bump particle if we're too close to an object
    private void BumpCheck()
    {
        float nearest_obj = 9999.99f;
        float distance = 0.0f;

        //Nearest nav mesh obj.
        foreach (NavMeshObstacle obj in FindObjectsOfType<NavMeshObstacle>())
        {
            distance = Vector3.Distance(this.transform.position, obj.transform.position);

            if (distance < nearest_obj)
            {
                nearest_obj = distance;
                ChangeDistance(obj.transform.lossyScale);

                if (Vector3.Distance(this.transform.position, obj.transform.position) <= new_bump)
                {
                    Bump();
                    //Debug.Break();
                }
                else
                {
                    has_bumped = false;
                }
            }
        }
    }

    //Changes bump distance in relation to obj scale
    private void ChangeDistance(Vector3 scale)
    {
        float largest_value = 0;
        if (scale.x > largest_value)
        {
            largest_value = transform.localScale.x;
        }
        if (scale.y > largest_value)
        {
            largest_value = transform.localScale.y;
        }
        if (scale.z > largest_value)
        {
            largest_value = transform.localScale.z;
        }

        new_bump = bump_range + largest_value + 1.2f;
    }

    //Go to nearest 'safe' space
    private void FindNearestWaypoint()
    {
        float max_distance = 9999.9f;
        Vector3 nearest_point = Vector3.zero;

        List<Vector3> waypoints = FindObjectOfType<EnemyManager>().waypoints;

        foreach (Vector3 point in waypoints)
        {
            float distance = Vector3.Distance(transform.position, point);

            if (distance < max_distance)
            {
                max_distance = distance;
                nearest_point = point;
            }
        }

        //We're already here, so find somewhere else to go
        if(Vector3.Distance(nearest_point, transform.position) <= bump_range)
        {
            int random = Random.Range(0, waypoints.Count);
            nearest_point = waypoints[random]; 
        }

       // Debug.Log("Moving to " + nearest_point);
        UpdateTargets(nearest_point);
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 player_pos = player.transform.position;

            //If the player is too close, we run away
            if (Vector3.Distance(transform.position, player_pos) <= EnemyRunDistance)
            {
                Vector3 dirToPlayer = transform.position - player_pos;

                Vector3 newPos = transform.position + dirToPlayer;

                Run();
                UpdateTargets(newPos);

               // Debug.Log("<color=red>AAHHH!!! SCARY PLAYER!!!</color>");

                return;
            }
        }
        CrippledWalk();
    }
}