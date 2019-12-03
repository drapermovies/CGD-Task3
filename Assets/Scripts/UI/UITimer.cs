using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UITimer : MonoBehaviour
{
    private Text timerText;
    public float timer;

    public bool countDown = false;

    // Start is called before the first frame update
    void Start()
    {
        timer *= 60;
        timerText = gameObject.GetComponent<Text>();
        var minutes = Mathf.Floor(timer / 60); //Divide the guiTime by sixty to get the minutes.
        var seconds = timer % 60;//Use the euclidean division for the seconds.
        var fraction = (timer * 100) % 100;

        //update the label value
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        timer--;
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown)
        {
            timer -= Time.deltaTime;
            if(timer <= 15.0f)
            {
                timerText.color = Color.red;
                Vector3 newScale = transform.localScale;
                newScale.x = Mathf.PingPong(Time.time, 0.5f) + 1;
                newScale.y = newScale.x;
                newScale.z = newScale.y;
                transform.localScale = newScale;

                if (timer <= 0.0f)
                {
                    //failure state
                    //GAME OVER
                    Debug.Log("Game Over");
                    PlayerPrefs.SetFloat("GameState", 2);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                }
            }

            var minutes = Mathf.Floor(timer / 60); //Divide the guiTime by sixty to get the minutes.
            var seconds = timer % 60;//Use the euclidean division for the seconds.
            var fraction = (timer * 100) % 100;

            //update the label value
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        }        
    }
}
