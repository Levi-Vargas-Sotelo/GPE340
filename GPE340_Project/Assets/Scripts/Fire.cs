using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnTriggerEnter(Collider collider)
    {
        //if collided with something then check if it has health
        Health health = collider.GetComponent<Health>();
        if (health)
        {
            //if it does then damage
            DealDamage(health);
        }
    }
}
