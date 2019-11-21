﻿using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CharMovement : MonoBehaviour
{

    public float jumpSpeed = 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private Animator anim;
    public Rigidbody rb;
    public float vSpeed;

    [SerializeField] private Transform target_marker = null;

    private NavMeshAgent[] nav_agents;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        anim.SetBool("isIdle", true);
    }
    void Start()
    {
        nav_agents = FindObjectsOfType<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
        vSpeed = rb.velocity.y;
        anim.SetFloat("vSpeed", vSpeed);
    }
    void Update()
    {
        CheckIdleState();

        if (Input.GetKeyDown("space") && anim.GetBool("isIdle"))
        {
            Jump();
        }

        //Debug function that allows the monster to walk to the cursor
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit_info;

            if (Physics.Raycast(ray.origin, ray.direction, out hit_info))
            {
                Vector3 target_pos = Vector3.zero;

                target_pos = hit_info.point;
                CrippledWalk();
                UpdateTargets(target_pos);
                target_marker.position = target_pos;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit_info;

            if (Physics.Raycast(ray.origin, ray.direction, out hit_info))
            {
                Vector3 target_pos = Vector3.zero;

                target_pos = hit_info.point;
                Run();
                UpdateTargets(target_pos);
                target_marker.position = target_pos;
            }
        }
#endif
#if !UNITY_EDITOR
        Debug.Log("We're a standalone");
        Destroy(target_marker.gameObject);
#endif 
    }

    public void Jump()
    {
        if (grounded && rb.velocity.y == 0)
        {
            anim.SetTrigger("isJump");
            rb.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);

        //Line at the mouse position
        Debug.DrawLine(target_marker.position,
    target_marker.position + Vector3.up * 5);
    }

    public void UpdateTargets(Vector3 target_pos)
    {
        foreach (NavMeshAgent agent in nav_agents)
        {
            agent.destination = target_pos;
        }
    }

    private void CheckIdleState()
    {
        foreach(NavMeshAgent agent in nav_agents)
        {
            if(transform.position == agent.destination)
            {
                Idle();
            }
        }
    }

    public void CrippledWalk()
    {
        anim.SetBool("crippled", !(anim.GetBool("crippled")));
        anim.SetBool("isIdle", false);

        GetComponent<NavMeshAgent>().speed = 1.3f; //Move slower
    }

    public void Idle()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isRun", false);
        anim.SetBool("crippled", false);
        anim.SetBool("dancing", false);

        Debug.Log("Idle");
    }

    public void Run()
    {
        anim.SetBool("isRun", !(anim.GetBool("isRun")));
        anim.SetBool("isIdle", false);

        GetComponent<NavMeshAgent>().speed = 3.5f; //Move faster
    }
}
