using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyCounter : MonoBehaviour
{
    private Text counterText;
    private int counter;
    private string startText;
    public int maxCounter;

    private RectTransform rectTransform;
    private Vector2 pos = new Vector3(0, -0.15f);
    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        counter = maxCounter;
        counterText = gameObject.GetComponent<Text>();
        startText = counterText.text;
        rectTransform = GetComponent<RectTransform>();
        counterText.text = startText + "0/" + maxCounter;
    }

    private void Update()
    {
        if (rectTransform.anchoredPosition != pos)
        {
            rectTransform.anchoredPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, pos, ref velocity, 0.7f);
        }
    }

    public void updateCounter()
    {
        counter--;
        counterText.text = startText + counter + "/" + maxCounter;
        if(counter == 0)
        {
            //win state
            //YOU WIN
        }
    }
}
