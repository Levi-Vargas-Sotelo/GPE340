using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //variables
    [SerializeField]
    public int Damage;
    [SerializeField]
    public Rigidbody projectileRB;
    [SerializeField]
    public float destroyTime;
    public Particles sparks;

    void Awake ()
    {
        //get own rigidbody
        projectileRB = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //delete the pickup in set amount of time
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter (Collider collider)
    {
        //if collided with something then check if it has health
        Health health = collider.GetComponent<Health>();
        if (health)
        {
            //if it does then damage
            DealDamage(health);
            if(sparks)
            {
                Instantiate(sparks, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
        Destroy(gameObject);
    }

    public void DealDamage (Health healthToLower)
    {
        healthToLower.Damage(Damage);
    }
}
