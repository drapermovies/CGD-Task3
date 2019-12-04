using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    //The list of colliders currently inside the trigger
    private List<Collider> TriggerList = new List<Collider>();
    public Camera capCamera;


    // Start is called before the first frame update
    void Start()
    {
        capCamera.gameObject.SetActive(false);
    }

    public void Capture()
    {
        foreach(Collider col in TriggerList)
        {
            if(col.gameObject.tag == "Enemy")
            {
                Destroy(col.gameObject);
                FindObjectOfType<UIEnemyCounter>().updateCounter();
                capCamera.gameObject.SetActive(true);
            }
        }
        //if no enemy play failed capture animation
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
