using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public float timeToDie;
    
    void Start ()
    {
        Destroy(gameObject, timeToDie);
    }
}
