using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(collision.gameObject);
                FindObjectOfType<UIEnemyCounter>().updateCounter();
                //play animation
            }
        }
    }
}
