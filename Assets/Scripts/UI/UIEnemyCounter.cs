using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyCounter : MonoBehaviour
{
    private Text counterText;
    private int counter;
    private string startText;
    // Start is called before the first frame update
    void Start()
    {
        counterText = gameObject.GetComponent<Text>();
        startText = counterText.text;
    }

    public void updateCounter()
    {
        counter--;
        counterText.text = startText + counter;
    }
}
