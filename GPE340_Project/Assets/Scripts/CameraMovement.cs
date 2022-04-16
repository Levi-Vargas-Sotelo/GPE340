using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offsetFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.Instance.thePlayer.transform;    
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            gameObject.transform.position = target.position + offsetFromTarget;
        }
    }
}
