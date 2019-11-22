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
    float accel = 5.0f;
    float jumpAccel = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vel.horizontal += Input.GetAxis("Horizontal") * accel;
        vel.horizontal *= Time.deltaTime;

        transform.position += transform.right * vel.horizontal;

        //deceleration
        if (vel.horizontal > 0.0f)
        {
            vel.horizontal -= 0.5f * Time.deltaTime;
            if (vel.horizontal < 0.0f)
            {
                vel.horizontal = 0.0f;
            }
        }
        else if (vel.horizontal < 0.0f)
        {
            vel.horizontal += 0.5f * Time.deltaTime;
            if (vel.horizontal > 0.0f)
            {
                vel.horizontal = 0.0f;
            }
        }

        vel.forward += Input.GetAxis("Vertical") * accel;
        vel.forward *= Time.deltaTime;

        transform.position += transform.forward * vel.forward;

        //deceleration
        if (vel.forward > 0.0f)
        {
            vel.forward -= 0.5f * Time.deltaTime;
            if (vel.forward < 0.0f)
            {
                vel.forward = 0.0f;
            }
        }
        else if (vel.forward < 0.0f)
        {
            vel.forward += 0.5f * Time.deltaTime;
            if (vel.forward > 0.0f)
            {
                vel.forward = 0.0f;
            }
        }
    }
}
