using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignRotation : MonoBehaviour
{
    //variables for target quaternion and speed 
    [SerializeField, Tooltip("The Transform to match rotations with")]
    private Transform target;
    [SerializeField, Tooltip("The speed to match the target's rotation")]
    private float speed = 5f;

    private void Update()
    {
        //slerp which interpolates 2 quaternions in a specified time;
        transform.rotation = Quaternion.Slerp (transform.rotation, target.rotation, speed * Time.deltaTime);
    }
}
