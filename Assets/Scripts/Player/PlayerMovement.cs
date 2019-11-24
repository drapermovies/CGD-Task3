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

    private Rigidbody rigidbody;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a"))
        {
            vel.horizontal -= accel * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            vel.horizontal += accel * Time.deltaTime;
        }
        vel.horizontal = Mathf.Clamp(vel.horizontal, -8.0f, 8.0f);
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
        }

        if (Input.GetKey("w"))
        {
            vel.forward += accel * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            vel.forward -= accel * Time.deltaTime;
        }
        vel.forward = Mathf.Clamp(vel.forward, -8.0f, 8.0f);
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
        }
    }

    private void FixedUpdate()
    {
        //jump
        if (rigidbody.velocity.y == 0.0f)
        {
            rigidbody.AddForce(new Vector3(0, 30, 0) * Input.GetAxis("Jump"), ForceMode.Impulse);
        }

        if (vel.forward != 0.0f || vel.horizontal != 0.0f)
        {
            Vector3 camForward = camera.transform.forward;
            Vector3 camRight = camera.transform.right;
            Vector3 camUp = camera.transform.up;
            
            camForward.y = 0f;
            camRight.y = 0f;
            camUp.y = 0f;
            camUp.Normalize();
            camForward.Normalize();
            camRight.Normalize();

            //camera direction with player velocity applied
            Vector3 desiredMoveDirection = camForward * vel.forward + camRight * vel.horizontal;

            //Move relative to camera's rotation
            Vector3 pos = transform.position + desiredMoveDirection * Time.deltaTime;
            transform.position = pos;

            //face direction camera is facing
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(camForward, camUp), turnSpeed);
        }
    }
}
