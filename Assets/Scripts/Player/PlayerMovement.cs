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
    float jumpAccel = 0.0f;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
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

        //jump
        //Vector3 rgVel = rigidbody.velocity;
       
        //else if (rgVel.y > 0.0f)
        //{
        //    rgVel.y -= 9.81f * Time.deltaTime;
        //    rigidbody.velocity = rgVel;
        //}


        //rotation
        Vector3 rotate = Vector3.zero;
        rotate.y = Input.GetAxis("Mouse X");

        transform.Rotate(rotate * Time.deltaTime * 325.0f);
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.y == 0.0f)
        {
            rigidbody.AddForce(new Vector3(0, 30, 0) * Input.GetAxis("Jump"), ForceMode.Impulse);
        }
    }
}
