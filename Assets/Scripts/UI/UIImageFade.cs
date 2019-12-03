using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageFade : MonoBehaviour
{
    public Text text;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var imageColor = image.color;
        var textColor = text.color;

        if (imageColor.a < 255)
        {
            imageColor.a += Time.deltaTime * 0.5f;
            textColor.a = imageColor.a;
            image.color = imageColor;
            text.color = textColor;
        }
    }
}
