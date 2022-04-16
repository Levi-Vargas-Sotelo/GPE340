using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private bool useLeftHandIK;
    public Transform transformLeftHandIK;
    public Transform transformLeftHandSolver;
    [SerializeField]
    private bool useRightHandIK;
    public Transform transformRightHandIK;
    public Transform transformRightHandSolver;
    public Pickup thisGunPrefab;
    [Header("Enemy Settings")]
    //stuff for the AI
    [SerializeField]
    public float AttackAngle;
    [SerializeField]
    public float MaxRange;
    


    public virtual void PullTrigger ()
    {
    }

    public virtual void ReleaseTrigger ()
    {
    }
}
