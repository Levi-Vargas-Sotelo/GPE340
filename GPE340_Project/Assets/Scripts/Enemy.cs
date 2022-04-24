using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : WeaponAgent
{
    [Header("Enemy Settings")]
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Animator anim;
    private Vector3 desiredVelocity;
    [SerializeField]
    private Weapon [] defaultWeapons;
    [SerializeField]
    public Health healthEnemy;
    [SerializeField]
    private CapsuleCollider mainCollider;
    [SerializeField]
    private Rigidbody enemyRB;
    [SerializeField]
    private bool Dead;
    [SerializeField]
    private float deathTime;
    [SerializeField]
    private float attackTime;

    // Start is called before the first frame update
    override public void Awake()
    {
        //assign variables and equip weapon
        base.Awake ();
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        EquipWeapon(defaultWeapons [Random.Range (0, defaultWeapons.Length)], 0);
        healthEnemy = GetComponent<Health>();
        mainCollider = GetComponent<CapsuleCollider>();
        enemyRB = GetComponent<Rigidbody>();
        GameManager.Instance.SpawnPlayer.AddListener (GetPlayer);
    }

    void Start()
    {
        GetPlayer();
        UIManager.UIMInstance.RegisterEnemy(this);
    }

    void GetPlayer()
    {
        //Debug.Log("");
        target = GameManager.Instance.thePlayer.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //if enemy isnt dead
        if(!Dead)
        {
            //if there is no player then just idle
            if (!target)
            {
                navMeshAgent.isStopped = true;
                anim.SetFloat ("Horizontal", 0f);
                anim.SetFloat ("Vertical", 0f);
                return;
            }

            //navigate to player
            navMeshAgent.SetDestination (target.transform.position);
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isPaused)
        {
            return;
        }
        
        if(!Dead)
        {
            if(target)
            {
                //shoot player
                if (Vector3.Angle(target.transform.forward, transform.position - target.transform.position) < equippedWeapon.AttackAngle)
                {
                    if(equippedWeapon)
                    {
                        StartCoroutine(ShootAndRelease());
                        //equippedWeapon.PullTrigger();
                    }
                        
                }
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject, deathTime);
        RagdollOn();
        Dead = true;
        StopAllCoroutines();
        Unequip();
    }

    //ragdoll settings
     public void RagdollOn ()
    {
        enemyRB.isKinematic = true;
        enemyRB.useGravity = true;
        anim.enabled = false;
        mainCollider.enabled = false;
        TurnOffComponentsInChildren();
    }

    public void RagdollOff ()
    {
        enemyRB.isKinematic = false;
        anim.enabled = true;
        mainCollider.enabled = true;
        TurnOnComponentsInChildren();
    }

    private void TurnOnComponentsInChildren ()
    {
        //loop for enabling children rigidbodies
        int i;
        Rigidbody[] childrenRB = GetComponentsInChildren<Rigidbody>();
        Collider[] childrenColl = GetComponentsInChildren<CapsuleCollider>();
        for (i = 0; i < childrenRB.Length; i++) 
        {
            childrenRB[i].isKinematic = true;
        }
        for (i = 0; i < childrenColl.Length; i++) 
        {
            childrenColl[i].enabled = false;
        }
    }

    private void TurnOffComponentsInChildren ()
    {
        mainCollider.enabled = false;
        //loop for enabling children rigidbodies
        int i;
        Rigidbody[] childrenRB = GetComponentsInChildren<Rigidbody>();
        Collider[] childrenColl = GetComponentsInChildren<CapsuleCollider>();
        for (i = 0; i < childrenRB.Length; i++) 
        {
            childrenRB[i].isKinematic = false;
        }
        for (i = 0; i < childrenColl.Length; i++) 
        {
            childrenColl[i].enabled = true;
        }
        mainCollider.enabled = false;
        enemyRB.isKinematic = true;
    }

    void OnAnimatorMove () 
    {
        //make animations work similarly to player
        Vector3 input = navMeshAgent.desiredVelocity;
        input = transform.InverseTransformDirection (input);
        anim.SetFloat ("Horizontal", input.x);
        anim.SetFloat ("Vertical", input.z); 
        navMeshAgent.velocity = anim.velocity;
        desiredVelocity = Vector3.MoveTowards(desiredVelocity, navMeshAgent.desiredVelocity, navMeshAgent.acceleration * Time.deltaTime);
        input = transform.InverseTransformDirection (desiredVelocity);
    }

    IEnumerator ShootAndRelease()
    {
        if(!Dead)
        {
            equippedWeapon.PullTrigger();
            yield return new WaitForSeconds(attackTime);
            equippedWeapon.ReleaseTrigger();
            yield return new WaitForSeconds(attackTime);
        }
        
    }

    protected virtual void OnAnimatorIK()
    {
        //Right arm settings
        
        // if there is an equipped wewapon
        if (!equippedWeapon)
            return;
        //if the equipped weapon has a transform for the right hand IK
        if (equippedWeapon.transformRightHandIK)
        {
            //move hand ik to where it should be
            anim.SetIKPosition (AvatarIKGoal.RightHand, equippedWeapon.transformRightHandIK.position);
            anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 1f);
            //rotate hand ik to how it should be
            anim.SetIKRotation (AvatarIKGoal.RightHand, equippedWeapon.transformRightHandIK.rotation);
            anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1f);
        }
        else
        {
            //if there none then turn it off
            anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 0f);
            anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 0f);
        } 

        //if weapon has ik hint solver for right elbow
        if (equippedWeapon.transformRightHandSolver)
        {
            //move hint to where it should be
            anim.SetIKHintPosition (AvatarIKHint.RightElbow, equippedWeapon.transformRightHandSolver.position);
            anim.SetIKHintPositionWeight (AvatarIKHint.RightElbow, 1f);
        }
        else
        {
            //if not then do not use it
            anim.SetIKHintPositionWeight (AvatarIKHint.RightElbow, 0f);
        }

        //Left arm setttings

        //if the equipped weapon has a transform for the left hand IK
        if (equippedWeapon.transformLeftHandIK)
        {
            //move hand ik to where it should be
            anim.SetIKPosition (AvatarIKGoal.LeftHand, equippedWeapon.transformLeftHandIK.position);
            anim.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1f);
            //rotate hand ik to how it should be
            anim.SetIKRotation (AvatarIKGoal.LeftHand, equippedWeapon.transformLeftHandIK.rotation);
            anim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1f);
        }
        else
        {
            //if there none then turn it off
            anim.SetIKPositionWeight (AvatarIKGoal.LeftHand, 0f);
            anim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 0f);
        }

        //if weapon has ik hint solver for left elbow
        if (equippedWeapon.transformLeftHandSolver)
        {
            //move hint to where it should be
            anim.SetIKHintPosition (AvatarIKHint.LeftElbow, equippedWeapon.transformLeftHandSolver.position);
            anim.SetIKHintPositionWeight (AvatarIKHint.LeftElbow, 1f);
        }
        else
        {
            //if not then do not use it
            anim.SetIKHintPositionWeight (AvatarIKHint.LeftElbow, 0f);
        }
    }
}
