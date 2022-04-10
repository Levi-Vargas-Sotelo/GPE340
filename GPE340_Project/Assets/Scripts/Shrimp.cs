using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrimp : Projectile
{
    //variables
    [SerializeField]
    private Projectile explosion;
    [SerializeField]
    private float timeToExplode;

    public override void OnTriggerEnter (Collider collider)
    {
        //explode if collided
        Projectile explosive = Instantiate (explosion, this.transform.position, this.transform.rotation) as Projectile;
        explosion.Damage = Damage;
        explosion.destroyTime = destroyTime;
        explosion.gameObject.layer = gameObject.layer; 
        base.OnTriggerEnter(collider);
    }

    void Explode ()
    {
        Projectile explosive = Instantiate (explosion, this.transform.position, this.transform.rotation) as Projectile;
        explosion.Damage = Damage;
        explosion.destroyTime = destroyTime;
        explosion.gameObject.layer = gameObject.layer;
    }
}
