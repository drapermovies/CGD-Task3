using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    Vector3 defaultPos;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = target.position;
    }

    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, defaultPos.y, -6));
        
        //rotation
        Vector3 rotate = Vector3.zero;
        rotate.x = Input.GetAxis("Mouse Y");
        //transform.RotateAround(target.position, rotate, Time.deltaTime * 20);

        // Smoothly move the camera towards that target position
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothTime);
    }
}
