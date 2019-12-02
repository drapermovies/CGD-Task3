using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    private Text timerText;
    public float timer;
    private string startText;
    // Start is called before the first frame update
    void Start()
    {
        timerText = gameObject.GetComponent<Text>();
        startText = timerText.text;
        timer *= 60;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        var minutes = timer / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = timer % 60;//Use the euclidean division for the seconds.
        var fraction = (timer * 100) % 100;

        //update the label value
        timerText.text = startText + string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
    }
}
