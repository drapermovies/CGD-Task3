using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private struct velocity
    {
        public float forward;
        public float horizontal;
        public float vertical;
    }

    private velocity vel;
    private float accel = 100.0f;
    private float turnSpeed = 0.1f;

    new private Rigidbody rigidbody;
    new public Camera camera;
    private Animator anim;

    bool jumpApex = false;

    private Vector3 desiredMoveDirection;
    private float camTimer = 1.0f;
    public bool newMove = true;
    public float resetCamTimer = 1.0f;
    public float speed = 7.5f;

    public GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        camTimer = resetCamTimer;
        anim = gameObject.GetComponent<Animator>();
        anim.SetLayerWeight(1, 1.0f);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d")) && !newMove)
        {
            newMove = true;
        }
        else
        {
            if(vel.forward == 0 && vel.horizontal == 0)
            {
                Vector3 newVel = Vector3.zero;
                newVel.y = rigidbody.velocity.y;
                rigidbody.velocity = newVel;
            }
        }

        if (Input.GetKey("a"))
        {
            vel.horizontal -= accel * Time.deltaTime;
            if (!PlayerAudioManager.GetisInAir())
            {
                PlayerAudioManager.Setiswalking(true);
            }
        }
        if (Input.GetKey("d"))
        {
            vel.horizontal += accel * Time.deltaTime;
            if (!PlayerAudioManager.GetisInAir())
            {
                PlayerAudioManager.Setiswalking(true);
            }
        }
        vel.horizontal = Mathf.Clamp(vel.horizontal, -20.0f, 20.0f);
        //deceleration
        if (!Input.GetKey("a") || !Input.GetKey("d"))
        {
            if (vel.horizontal > 0.0f)
            {
                vel.horizontal -= 60.0f * Time.deltaTime;
                if (vel.horizontal < 0.0f)
                {
                    vel.horizontal = 0.0f;
                }
            }
            else if (vel.horizontal < 0.0f)
            {
                vel.horizontal += 60.0f * Time.deltaTime;
                if (vel.horizontal > 0.0f)
                {
                    vel.horizontal = 0.0f;
                }
            }

            ChangeBool("Idle", true);
        }
        // Stop input (for audio)
        if(Input.GetKeyUp("a"))
        {
            PlayerAudioManager.Setiswalking(false);
        }
        if(Input.GetKeyUp("d"))
        {
            PlayerAudioManager.Setiswalking(false);
        }


        if (Input.GetKey("w"))
        {
            vel.forward += accel * Time.deltaTime;
            if (!PlayerAudioManager.GetisInAir())
            {
                PlayerAudioManager.Setiswalking(true);
            }
        }
        if (Input.GetKey("s"))
        {
            vel.forward -= accel * Time.deltaTime;
            if (!PlayerAudioManager.GetisInAir())
            {
                PlayerAudioManager.Setiswalking(true);
            }
        }
        vel.forward = Mathf.Clamp(vel.forward, -20.0f, 20.0f);
        //deceleration
        if (!Input.GetKey("w") || !Input.GetKey("s"))
        {
            if (vel.forward > 0.0f)
            {
                vel.forward -= 60.0f * Time.deltaTime;
                if (vel.forward < 0.0f)
                {
                    vel.forward = 0.0f;
                }
            }
            else if (vel.forward < 0.0f)
            {
                vel.forward += 60.0f * Time.deltaTime;
                if (vel.forward > 0.0f)
                {
                    vel.forward = 0.0f;
                }
            }
            ChangeBool("Idle", true);
        }

        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {

           if (Input.GetKey("left shift"))
           {
                speed = 14.0f;
                if (!PlayerAudioManager.GetisInAir())
                {
                    PlayerAudioManager.Setiswalking(false);
                    PlayerAudioManager.SetisRunning(true);
                }
                    ChangeBool("Running", true);
           }
           
           else
           {
                speed = 7.5f;
                if (!PlayerAudioManager.GetisInAir())
                {
                    PlayerAudioManager.SetisRunning(false);
                    PlayerAudioManager.Setiswalking(true);
                }
                ChangeBool("Walking", true);
           }

        }
        else
        {
            PlayerAudioManager.SetisRunning(false);
        }

        if (Input.GetMouseButton(1) && (Input.GetKey("left shift")) == false)
        {
            if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Jumping")))
            {
                anim.SetBool("Capturing", false);
                crosshair.gameObject.SetActive(false);
            }

            else
            {
                anim.SetBool("Capturing", true);
                speed = 5.0f;
                crosshair.gameObject.SetActive(true);
            }
        }

        else
        {
            anim.SetBool("Capturing", false);
            crosshair.gameObject.SetActive(false);
        }
        if(anim.GetBool("Capturing") && Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<PlayerCapture>().Capture();
        }
        // Stop input (for audio)
        if (Input.GetKeyUp("w"))
        {
            PlayerAudioManager.Setiswalking(false);
        }
        if (Input.GetKeyUp("s"))
        {
            PlayerAudioManager.Setiswalking(false);
        }

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (rigidbody.velocity.y <= 0.2f && rigidbody.velocity.y >= -0.2f)
            {
                if (Physics.Raycast(transform.position, -Vector3.up, gameObject.GetComponent<Collider>().bounds.extents.y + 0.1f))
                {
                    rigidbody.AddForce(new Vector3(0, 30, 0), ForceMode.Impulse);
                    PlayerAudioManager.SetisJumping(true);
                    PlayerAudioManager.Setiswalking(false);
                    PlayerAudioManager.SetisInAir(true);
                    anim.SetTrigger("JumpingTrigger");
                }
            }
        }
        else
        {
            PlayerAudioManager.SetisJumping(false);
            if (rigidbody.velocity.y == 0)
            {
                if (jumpApex == false)
                {
                    if (PlayerAudioManager.GetisInAir() == true)
                    {
                        jumpApex = true;
                    }
                }
                else
                {
                    PlayerAudioManager.SetisInAir(false);
                    PlayerAudioManager.SetisLanding(true);
                    jumpApex = false;
                }
            }
            
        }

        if (anim.GetBool("Capturing"))
        {
            Vector3 camForward = camera.transform.forward;
            Vector3 camRight = camera.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            var halfWayVector = (camForward + camRight).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(halfWayVector), turnSpeed);
        }

        if (vel.forward != 0.0f || vel.horizontal != 0.0f)
        {
            Vector3 camForward = camera.transform.forward;
            Vector3 camRight = camera.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            //camera direction with player velocity applied
            if(newMove)
            {
                desiredMoveDirection = ((camForward * vel.forward) + (camRight * vel.horizontal)) * Time.deltaTime * speed;
            }

            //rotate
            if(!anim.GetBool("Capturing"))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), turnSpeed);
            }
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            //Move relative to camera's rotation
            //transform.position = transform.position + desiredMoveDirection * Time.deltaTime;
            desiredMoveDirection.y = rigidbody.velocity.y;
            rigidbody.velocity = desiredMoveDirection;


            //if camera isnt facing general direction of player movement
            //countdown and set rotation to face player direction
            if (Vector3.Angle(camForward, transform.forward) > 50.0f && newMove)
            {
                camTimer -= Time.deltaTime;
                if (camTimer <= 0.0f)
                {
                    camera.GetComponent<FollowCamera>().rotateToPlayer = true;
                    camera.GetComponent<FollowCamera>().SetNewDirections(transform.up, transform.forward, transform.right);
                    newMove = false;
                }
            }
            else
            {
                ResetCamTimer();
            }
        }
        else
        {
            ResetCamTimer();
        }
    }
    public void ResetCamTimer()
    {
        if (camTimer < 1.0f)
        {
            camTimer = resetCamTimer;
        }
    }

    private void ChangeBool(string toggleBool, bool boolState)
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool(toggleBool, boolState);
        return;
    }
}