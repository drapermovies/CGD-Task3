using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    public Transform target;
    public Transform player;
    public float turnSpeed = 10.0f;
    private Vector3 offset;
    private Vector3 defaultOffset;

    public bool rotateToPlayer = false;

    private Vector3 playerForward;
    private Vector3 playerRight;
    private Vector3 playerUp;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0.5f, -1.75f);
        defaultOffset = offset;
    }

    void LateUpdate()
    {
        if (!rotateToPlayer)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                if (!player.GetComponent<PlayerMovement>().newMove)
                {
                    player.GetComponent<PlayerMovement>().newMove = true;
                }
                else
                {
                    player.GetComponent<PlayerMovement>().ResetCamTimer();
                }
            }
            offset = Quaternion.Euler(0, Input.GetAxis("Mouse X"), 0) * offset;
            offset = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y"), transform.right) * offset;
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
        else
        {
            Vector3 camForward = transform.forward;
            camForward.y = 0;
            if (Vector3.Angle(playerForward, camForward) < 0.1f)
            {
                rotateToPlayer = false;
            }
            else
            {
                if (Vector3.Angle(playerForward, camForward) > 100f)
                {
                    offset = offset + (((-playerForward * defaultOffset.x) + (playerUp * defaultOffset.y) + (playerRight * defaultOffset.z) - offset));
                }
                else
                {
                    offset = offset + (((playerRight * defaultOffset.x) + (playerUp * defaultOffset.y) + (playerForward * defaultOffset.z) - offset) * Time.deltaTime * 10);
                }
                transform.position = target.position + offset;
                transform.LookAt(target);
            }
        }
    }

    public void SetNewDirections(Vector3 up, Vector3 forward, Vector3 right)
    {
        playerUp = up;
        playerRight = right;
        playerForward = forward;
    }
}
