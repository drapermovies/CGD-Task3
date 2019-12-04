using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    //The list of colliders currently inside the trigger
    private List<Collider> TriggerList = new List<Collider>();
    public Camera capCamera;
    private List<GameObject> enemyList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        capCamera.gameObject.SetActive(false);
    }

    public void Capture()
    {
        bool success = false;
        foreach(Collider col in TriggerList)
        {
            if(col.gameObject.tag == "Enemy")
            {
                if(!enemyList.Contains(col.gameObject))
                {
                    enemyList.Add(col.gameObject);
                    col.gameObject.SetActive(false);
                    FindObjectOfType<UIEnemyCounter>().updateCounter();
                    success = true;
                }
                
            }
        }
        if(success)
        {
            capCamera.gameObject.SetActive(true);
        }
        else
        {
            //play failed capture animation + sound
        }
    }

    //called when something enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        //if the object is not already in the list
        if (!TriggerList.Contains(other))
        {
            //add the object to the list
            TriggerList.Add(other);
        }
    }

    //called when something exits the trigger
    private void OnTriggerExit(Collider other)
    {
        //if the object is in the list
        if (TriggerList.Contains(other))
        {
            //remove it from the list
            TriggerList.Remove(other);
        }
    }
}
