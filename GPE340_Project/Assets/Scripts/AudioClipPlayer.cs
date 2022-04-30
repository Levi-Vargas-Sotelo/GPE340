using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipPlayer : MonoBehaviour
{
    [SerializeField]
    private float timeToDie;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(clip);
        Destroy(gameObject, timeToDie);
    }
}
