using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapCameraScript : MonoBehaviour
{
    public Camera mainCamera;
    public Camera capCamera;
    public GameObject player;
    public float loopCount;
    private float origY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            capCamera.enabled = true;
            mainCamera.enabled = false;
            mainCamera.GetComponent<FollowCamera>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
        }

        if (loopCount < 2)
        {
            cameraAnim();
        }
        

        if (loopCount >= 1.05)
        {
            loopCount = 0;
            capCamera.enabled = false;
            capCamera.gameObject.SetActive(false);

            mainCamera.enabled = true;
            mainCamera.GetComponent<FollowCamera>().enabled = true;
            player.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    void cameraAnim()
    {
        for (int count = 0; count < 2; count++)
        {
            GetComponent<Animator>().Play("CapCameraAnimation");
            loopCount += 0.01f;
        }
    }
}
