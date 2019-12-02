using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    private Text timerText;
    public float timer;
    private string startText;

    private RectTransform rectTransform;
    private Vector2 pos = new Vector3(0, 0.15f);
    private Vector2 velocity = Vector2.zero;

    private bool inPosition = false;
    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        timerText = gameObject.GetComponent<Text>();
        startText = timerText.text;
        timerText.text = startText + "03:00:00";
        timer *= 60;
        timer--;
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
            if(!inPosition)
            {
                inPosition = true;
                Destroy(image);
            }
            timer -= Time.deltaTime;

            if(timer <= 0.0f)
            {
                //failure state
            }

            var minutes = Mathf.Floor(timer / 60); //Divide the guiTime by sixty to get the minutes.
            var seconds = timer % 60;//Use the euclidean division for the seconds.
            var fraction = (timer * 100) % 100;

            //update the label value
            timerText.text = startText + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        }
    }
}
