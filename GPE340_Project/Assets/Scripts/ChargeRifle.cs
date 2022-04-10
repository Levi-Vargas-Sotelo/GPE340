using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeRifle : Weapon
{
    //variables to handle unique gun properties
    [Header("Charge Rifle Settings")]
    [SerializeField]
    private Projectile bulletPrefab;
    [SerializeField]
    private Transform barrelPoint;
    [SerializeField]
    private float damageRate;
    [SerializeField]
    private int lifeTime;
    [SerializeField]
    private int fireRate;
    [SerializeField]
    private int spread;
    [SerializeField]
    private bool charging = false;
    [SerializeField]
    private float damageDone;
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private float maxDamage;

    void start ()
    {
        //start with damage on no charge
        damageDone = baseDamage;
    }
    private void ShootBullets ()
    {
        //spawn bullets pass damage, lifetime,velocity and set layer
        Projectile projectile = Instantiate (bulletPrefab, barrelPoint.position, barrelPoint.rotation * Quaternion.Euler(Random.onUnitSphere * spread)) as Projectile;
        projectile.Damage = (int)damageDone;
        projectile.destroyTime = lifeTime;
        projectile.projectileRB.AddRelativeForce (Vector3.forward * fireRate, ForceMode.VelocityChange);
        projectile.gameObject.layer = gameObject.layer; 
        //reset charge
        damageDone = baseDamage;
    }

    public override void PullTrigger()
    {
        //if trigger is pulled
        charging = true;
        base.PullTrigger();
    }
    public override void ReleaseTrigger()
    {
        //if trigger is released
        charging = false;
        ShootBullets();
        base.ReleaseTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        //check mouse status
        if (Input.GetMouseButtonDown(0))
        {
            PullTrigger();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseTrigger();
        }

        if (charging)
        {
            damageDone += damageRate * Time.deltaTime;
            damageDone = Mathf.Clamp(damageDone, baseDamage, maxDamage);
        }
    }
}
