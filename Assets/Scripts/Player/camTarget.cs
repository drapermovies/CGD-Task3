using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTarget : MonoBehaviour
{
    public Transform target;
    new public Transform camera;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        transform.position = target.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        //position the target so it is always to the right of player relative to camera rotation
        transform.position = target.position + (camera.right * offset.x) + (target.up * offset.y);
    }
}
