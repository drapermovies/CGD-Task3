using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UITimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    public float timer;

    public bool countDown = false;

    private FMOD.Studio.EventInstance music;

    // Start is called before the first frame update
    void Start()
    {
        timer *= 60;
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        var minutes = Mathf.Floor(timer / 60); //Divide the guiTime by sixty to get the minutes.
        var seconds = timer % 60;//Use the euclidean division for the seconds.
        var fraction = (timer * 100) % 100;

        //update the label value
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        timer--;
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music");
        music.setParameterValue("Timer", timer);
        music.start();
        music.release();
    }

    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    // Update is called once per frame
    void Update()
    {
        music.setParameterValue("Timer", timer);
        if (countDown)
        {
            timer -= Time.deltaTime;
            if (timer <= 30.0f)
            {
                if(timerText.color != Color.red)
                {
                    timerText.color = Color.red;
                }                
                if (timer <= 15.0f)
                {
                    Vector3 newScale = transform.localScale;
                    newScale.x = Mathf.PingPong(Time.time, 0.5f) + 1;
                    newScale.y = newScale.x;
                    newScale.z = newScale.y;
                    transform.localScale = newScale;

                    if (timer <= 0.0f)
                    {
                        //failure state
                        //GAME OVER
                        //Debug.Log("Game Over");
                        PlayerPrefs.SetFloat("GameState", 2);
                        BGMManager.SetBGMend(true);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                    }
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
