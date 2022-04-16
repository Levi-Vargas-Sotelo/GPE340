using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    //variables to handle unique gun properties
    [Header("Pistol Settings")]
    [SerializeField]
    private Projectile bulletPrefab;
    [SerializeField]
    private Transform barrelPoint;
    [SerializeField]
    private bool triggerPulled;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private int lifeTime;
    [SerializeField]
    private int fireRate;
    [SerializeField]
    private int spread;

    private void ShootBullets ()
    {
        //spawn bullets pass damage, lifetime,velocity and set layer
        Projectile projectile = Instantiate (bulletPrefab, barrelPoint.position, barrelPoint.rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as Projectile;
        projectile.Damage = Damage;
        projectile.destroyTime = lifeTime;
        projectile.projectileRB.AddRelativeForce (Vector3.forward * fireRate, ForceMode.VelocityChange);
        projectile.gameObject.layer = gameObject.layer; 
    }

    public override void PullTrigger()
    {
        //if trigger is pulled
        ShootBullets();
        base.PullTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
