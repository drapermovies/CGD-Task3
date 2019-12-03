using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveText : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 pos = new Vector3(0, 0.15f);
    private Vector2 velocity = Vector2.zero;
    private bool inPosition = false;
    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
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
            if (!inPosition)
            {
                inPosition = true;
                GetComponentInChildren<UITimer>().countDown = true;
                Destroy(image);
            }
        }
    }
}
