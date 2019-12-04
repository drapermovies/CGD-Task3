using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioPlayer : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string audioWalk;
    public float walkingSpeed;
    public float runningSpeed;

    FMOD.Studio.EventInstance PlayerWalk;
    FMOD.Studio.ParameterInstance Surface;

    [FMODUnity.EventRef]
    public string audioLand;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
        InvokeRepeating("CallRunsteps", 0, runningSpeed);

        Surface.setValue(1);

        PlayerWalk = FMODUnity.RuntimeManager.CreateInstance(audioWalk);
        PlayerWalk.getParameter("Surface", out Surface);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Is Walking: " + PlayerAudioManager.GetisWalking());
        //Debug.Log("Is Jumping: " + PlayerAudioManager.GetisJumping());
        //Debug.Log("Is in Air: " + PlayerAudioManager.GetisInAir());
        //Debug.Log("Is Sucking: " + PlayerAudioManager.GetisSucking());
        //Debug.Log("Is Hit: " + PlayerAudioManager.GetisHit());

        if(PlayerAudioManager.GetisLanding() == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(audioLand);
            PlayerAudioManager.SetisLanding(false);
        }

    }

    void CallFootsteps()
    {
        if (PlayerAudioManager.GetisWalking() == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(audioWalk);
        }
    }
    void CallRunsteps()
    {
        if (PlayerAudioManager.GetisRunning() == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(audioWalk);
        }
    }

}
