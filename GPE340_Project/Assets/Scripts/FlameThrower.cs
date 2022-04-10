using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Weapon
{
    //variables to handle unique gun properties
    [Header("FlameThrower Settings")]
    [SerializeField]
    private Projectile bulletPrefab;
    [SerializeField]
    public Transform barrelPoint;
    [SerializeField]
    private bool triggerPulled;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private int lifeTime;
    [SerializeField]
    private int fireRate;
    private float timeNextShotIsReady;
    [SerializeField]
    private int shotsPerMinute;
    [SerializeField]
    private int spread;

    private void Awake()
    {
        //start timer
        timeNextShotIsReady = Time.time;
    } 

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
        triggerPulled = true;
        base.PullTrigger();
    }

    public override void ReleaseTrigger()
    {
        //if trigger is released
        triggerPulled = false;
        base.ReleaseTrigger();
    }

    void Update ()
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
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //if shooting 
        if (triggerPulled)
        {
            //check if its time
            if(Time.time > timeNextShotIsReady)
            {
                //shoot
                ShootBullets();
                timeNextShotIsReady += 60f / shotsPerMinute;
            }
        } 
        //reset time
        else if (Time.time > timeNextShotIsReady)
        {
            timeNextShotIsReady = Time.time;
        }
    }
}
