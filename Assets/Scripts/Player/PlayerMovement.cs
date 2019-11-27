﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private struct velocity
    {
        public float forward;
        public float horizontal;
        //public float vertical;
    }

    private velocity vel;
    private float accel = 100.0f;
    private float turnSpeed = 0.1f;

    new private Rigidbody rigidbody;
    new public Camera camera;

    private Vector3 desiredMoveDirection;
    private float camTimer = 1.0f;
    public bool newMove = true;
    public float resetCamTimer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        camTimer = resetCamTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d")) && !newMove)
        {
            newMove = true;
        }

        if (Input.GetKey("a"))
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
            
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            //camera direction with player velocity applied
            if (newMove)
            {
                desiredMoveDirection = camForward * vel.forward + camRight * vel.horizontal;
            }

            //Move relative to camera's rotation
            transform.position = transform.position + desiredMoveDirection * Time.deltaTime;

            //rotate
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), turnSpeed);

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
}