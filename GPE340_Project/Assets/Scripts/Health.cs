using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //variables
    [SerializeField]
    public float maxHealthValue;
    public float healthValue;

    //events to call
    [Header("Events")]
    [SerializeField]
    private UnityEvent OnHeal;
    [SerializeField]
    private UnityEvent OnDamage;
    [SerializeField]
    public UnityEvent OnDeath;

    //damage event
    public void Damage (float damageTaken)
    {
        healthValue -= damageTaken;
        //if health dropped to 0
        if (healthValue <= 0)
        {
            //die
            healthValue = 0;
            Kill();
        }
    }

    //heal event
    public void Heal (float healthReplenished)
    {
        healthValue += healthReplenished;
        //if maxed out then limit 
        if (healthValue >= maxHealthValue)
        {
            healthValue = maxHealthValue;
        }
    }

    //kill event
    public void Kill ()
    {
        OnDeath.Invoke();
    }

    //full heal event
    public void FullHeal ()
    {
        healthValue = maxHealthValue;
    }
}
