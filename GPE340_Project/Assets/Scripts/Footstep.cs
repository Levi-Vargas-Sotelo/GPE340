using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    [SerializeField]
    private AudioClip footstep;
    [SerializeField]
    private AudioSource audiosource;

    private void AnimationEventFootstepSound()
    {
        audiosource.PlayOneShot (footstep);
    }
}
