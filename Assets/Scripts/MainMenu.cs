using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject victoryMenu;
    public GameObject defeatMenu;

    public bool inMainMenu = true;
    public bool inOptionsMenu = false;
    public bool inVictoryMenu = false;
    public bool inDefeatMenu = false;
    public bool currentlyTransitioning = false;
    public bool checkedState = false;

    [SerializeField] MainMenuCamera mainCamera;
    [SerializeField] Animator animator;

    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus Ambience;

    float masterVolume = 1.0f;
    float musicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float ambienceVolume = 0.5f;

    private FMOD.Studio.EventInstance music;
    private FMOD.Studio.EventInstance ambience;

    // Start is called before the first frame update
    void Start()
    {
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Ambience = FMODUnity.RuntimeManager.GetBus("bus:/Master/Ambience");
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music");
        music.setParameterValue("Timer", 180);
        music.start();
        music.release();
        ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/Atmosphere");
        ambience.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.main.gameObject));
        ambience.start();
        ambience.release();
    }

    private void OnDestroy()
    {
        //ambience.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("GameState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float GameState = PlayerPrefs.GetFloat("GameState");
        if (!checkedState)
        {
            //victory
            if (GameState == 1)
            {
                inVictoryMenu = true;
            }
            //defeat
            else if (GameState == 2)
            {
                inDefeatMenu = true;
            }
            else
            {
                inMainMenu = true;
            }
            checkedState = true;
        }

        menuTypeConfiguration();
        cameraTransitions();

        Master.setVolume(masterVolume);
        Music.setVolume(musicVolume);
        SFX.setVolume(SFXVolume);
        Ambience.setVolume(ambienceVolume);

    }

    private void menuTypeConfiguration()
    {
        //in hindsight, should have been a case, but would be a bit of effort to change.. spare me
        if (inMainMenu)
        {
            mainMenu.gameObject.SetActive(true);
            optionsMenu.gameObject.SetActive(false);
            victoryMenu.gameObject.SetActive(false);
            defeatMenu.gameObject.SetActive(false);
        }
        else if (inOptionsMenu)
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(true);
            victoryMenu.gameObject.SetActive(false);
            defeatMenu.gameObject.SetActive(false);
        }
        else if (inVictoryMenu)
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            victoryMenu.gameObject.SetActive(true);
            defeatMenu.gameObject.SetActive(false);
        }
        else if (inDefeatMenu)
        {
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            victoryMenu.gameObject.SetActive(false);
            defeatMenu.gameObject.SetActive(true);
        }
    }

    private void cameraTransitions()
    {
        //camera transitions
        if (inOptionsMenu == true && currentlyTransitioning == true)
        {
            animator.SetBool("cameraMain", false);
            animator.SetBool("cameraOptions", true);
        }
        else if (inMainMenu == true && currentlyTransitioning == true)
        {
            animator.SetBool("cameraOptions", false);
            animator.SetBool("cameraMain", true);
        }
        else
        {
            animator.SetBool("cameraOptions", false);
            animator.SetBool("cameraMain", false);
        }
    }

    public void MasterVolumeLevel(float newVolume)
    {
        masterVolume = newVolume;
    }
    public void MusicVolumeLevel(float newVolume)
    {
        musicVolume = newVolume;
    }
    public void SFXVolumeLevel(float newVolume)
    {
        SFXVolume = newVolume;
    }
    public void AmbienceVolumeLevel(float newVolume)
    {
        ambienceVolume = newVolume;
    }
}
