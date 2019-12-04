using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioPlayer : MonoBehaviour
{
    [FMODUnity.EventRef]
    [SerializeField] private string walk_audio;

    [SerializeField] private float movement_speed = 1;

    private void Start()
    {
        InvokeRepeating("CallFootsteps", 0, movement_speed);
    }

    void CallFootsteps()
    {
        if (EnemyAudioManager.is_walking)
        {
            FMODUnity.RuntimeManager.PlayOneShot(walk_audio);
        }
    }
}
