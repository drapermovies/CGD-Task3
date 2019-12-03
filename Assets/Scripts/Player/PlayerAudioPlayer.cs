using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioPlayer : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string audioWalk;
    public float walkingSpeed;

    [FMODUnity.EventRef]
    public string audioJump;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Is Walking: " + PlayerAudioManager.GetisWalking());
        //Debug.Log("Is Jumping: " + PlayerAudioManager.GetisJumping());
        //Debug.Log("Is in Air: " + PlayerAudioManager.GetisInAir());
        //Debug.Log("Is Sucking: " + PlayerAudioManager.GetisSucking());
        //Debug.Log("Is Hit: " + PlayerAudioManager.GetisHit());

        if(PlayerAudioManager.GetisJumping() == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(audioJump);
        }

    }

    void CallFootsteps()
    {
        if (PlayerAudioManager.GetisWalking() == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot(audioWalk);
        }
    }

}
