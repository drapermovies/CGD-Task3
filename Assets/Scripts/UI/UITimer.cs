using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    private Text timerText;
    public GameObject enemyCounter;
    public float timer;
    private string startText;

    private RectTransform rectTransform;
    private Vector2 pos = new Vector3(0, 0.15f);
    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        timerText = gameObject.GetComponent<Text>();
        startText = timerText.text;
        timer *= 60;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.anchoredPosition != pos)
        {
            rectTransform.anchoredPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, pos, ref velocity, 0.7f);
        }
        else
        {
            timer -= Time.deltaTime;

            if(timer <= 0.0f)
            {
                //failure state
            }

            var minutes = timer / 60; //Divide the guiTime by sixty to get the minutes.
            var seconds = timer % 60;//Use the euclidean division for the seconds.
            var fraction = (timer * 100) % 100;

            //update the label value
            timerText.text = startText + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        }
    }
}
