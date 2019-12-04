using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    FMOD.Studio.EventInstance BGM;
    [FMODUnity.EventRef]
    public string musicPath;

    FMOD.Studio.ParameterInstance Timer;


    // Start is called before the first frame update
    void Start()
    {
        BGM = FMODUnity.RuntimeManager.CreateInstance(musicPath);
        BGM.getParameter("Timer", out Timer);
    }

    // Update is called once per frame
    void Update()
    {
        Timer.setValue(60);

        if(BGMManager.GetBGMstart())
        {
            BGM.start();
            BGMManager.SetBGMstart(false);
        }

        if(BGMManager.GetBGMend())
        {
            BGM.release();
            BGMManager.SetBGMend(false);
        }
    }
}
