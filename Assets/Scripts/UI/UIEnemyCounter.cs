using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyCounter : MonoBehaviour
{
    private Text counterText;
    private int counter;
    private string startText;

    private RectTransform rectTransform;
    private Vector2 pos = new Vector3(0, -0.15f);
    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        counterText = gameObject.GetComponent<Text>();
        startText = counterText.text;
        rectTransform = GetComponent<RectTransform>();
        counterText.text += "0/5";
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
        counterText.text = startText + counter + "/5";
    }
}
