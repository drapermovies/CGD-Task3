using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    public Transform target;
    public float turnSpeed = 4.0f;
    private Vector3 offsetX;
    private Vector3 offsetY;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = new Vector3(target.position.x, target.position.y + 3.0f, target.position.z - 4.0f);
    }

    void LateUpdate()
    {
        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
        transform.position = target.position + offsetX;
        transform.LookAt(target.position);
    }
}
