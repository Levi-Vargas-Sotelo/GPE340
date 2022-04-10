using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField]
    //variable for destroy timer
    private float destroyTime;
    private Player player;
    void Start ()
    {
        //delete the pickup in set amount of time
        Destroy(gameObject, destroyTime);
    }

    void Update ()
    {
        //spin it
        transform.Rotate(new Vector3(45, 45, 45) * Time.deltaTime);
    }

    public void OnTriggerEnter (Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player)
        {
            OnPickUp(player);
        }
    } 

    public virtual void OnPickUp (Player player) 
    {
        Destroy(gameObject);
    }
}
