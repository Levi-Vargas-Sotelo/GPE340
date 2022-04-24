using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WeaponAgent
{
    [Header("Player Attributes")]
    //variables to use  
    [SerializeField]  
    //player speed
    private float speed = 4f; 
    [SerializeField] 
    private float runSpeed = 6f;
    //ref for the animator
    public Animator animator;
    public Health playerHealth;
    [SerializeField] 
    private Weapon theCurrentEquippedWeapon;
    [SerializeField]
    private Weapon defaultGun;
    [SerializeField]
    private float deathTime;
    [SerializeField]
    private bool Dead;
    [Header("Ragdoll Attributes")]
    [SerializeField]
    private CapsuleCollider mainCollider;
    [SerializeField]
    private Rigidbody playerRB;

    override public void Awake()
    {
        base.Awake();
        //obtain the animator and health at the very start before first frame is drawn
        animator = GetComponent<Animator>();
        EquipWeapon (defaultGun, 1);
        mainCollider = GetComponent<CapsuleCollider>();
        playerRB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
        UIManager.UIMInstance.RegisterPlayer (this);
    }

    private void Update()
    {
        //if the player presses and holds shift then start running
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Run();
        }

        //if player lets go of shift then walk again
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 4f;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Unequip();
        }

        if (!Dead)
        {
            if(GameManager.Instance.isPaused == false)
            {
                if(equippedWeapon)
                {
                    //check mouse status
                    if (Input.GetMouseButtonDown(0))
                    {
                        equippedWeapon.PullTrigger();
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        equippedWeapon.ReleaseTrigger();
                    }
                }
                
            }
            
        } 
    }

    //moving player world space
    public void Move( Vector3 input)
    {
        //convert input into world space
        input = transform.InverseTransformDirection(input);

        //clamp the vector to be 1
        input = Vector3.ClampMagnitude (input, 1f);
        //move by multiplying vector by set speed
        input *= speed;
        //play according animations with both parameters on the blend tree
        animator.SetFloat ("Horizontal", input.x);
		animator.SetFloat ("Vertical", input.z);
    }

    //look at mouse 
    public void LookAtMouse(Vector3 mousePosition)
    {
        //look at the mouse while retaining position and world space movement
        transform.rotation = Quaternion.LookRotation (mousePosition - transform.position);
    }

    //function to increase movement speed for running
    public void Run()
    {
        speed = runSpeed;
    }

    //when die 
    public void Die()
    {
        Destroy(gameObject, deathTime);
        Unequip();
        RagdollOn();
        Dead = true;
    }

    //equip weapon
    override public void EquipWeapon(Weapon weapon, int gunType)
    {
        base.EquipWeapon(weapon, gunType);
        theCurrentEquippedWeapon = equippedWeapon;
        animator.SetInteger ("WeaponHold", gunType);
        OnAnimatorIK();
    }

    //ragdoll settings
    public void RagdollOn ()
    {
        playerRB.isKinematic = true;
        animator.enabled = false;
        mainCollider.enabled = false;
        TurnOffComponentsInChildren();
    }

    public void RagdollOff ()
    {
        animator.enabled = true;
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
        playerRB.isKinematic = true;
    }

    //move hand iks
    protected virtual void OnAnimatorIK()
    {
        //Right arm settings
        
        // if there is an equipped wewapon
        if (!theCurrentEquippedWeapon)
            return;
        //if the equipped weapon has a transform for the right hand IK
        if (theCurrentEquippedWeapon.transformRightHandIK)
        {
            //move hand ik to where it should be
            animator.SetIKPosition (AvatarIKGoal.RightHand, theCurrentEquippedWeapon.transformRightHandIK.position);
            animator.SetIKPositionWeight (AvatarIKGoal.RightHand, 1f);
            //rotate hand ik to how it should be
            animator.SetIKRotation (AvatarIKGoal.RightHand, theCurrentEquippedWeapon.transformRightHandIK.rotation);
            animator.SetIKRotationWeight (AvatarIKGoal.RightHand, 1f);
        }
        else
        {
            //if there none then turn it off
            animator.SetIKPositionWeight (AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight (AvatarIKGoal.RightHand, 0f);
        } 

        //if weapon has ik hint solver for right elbow
        if (theCurrentEquippedWeapon.transformRightHandSolver)
        {
            //move hint to where it should be
            animator.SetIKHintPosition (AvatarIKHint.RightElbow, theCurrentEquippedWeapon.transformRightHandSolver.position);
            animator.SetIKHintPositionWeight (AvatarIKHint.RightElbow, 1f);
        }
        else
        {
            //if not then do not use it
            animator.SetIKHintPositionWeight (AvatarIKHint.RightElbow, 0f);
        }

        //Left arm setttings

        //if the equipped weapon has a transform for the left hand IK
        if (theCurrentEquippedWeapon.transformLeftHandIK)
        {
            //move hand ik to where it should be
            animator.SetIKPosition (AvatarIKGoal.LeftHand, theCurrentEquippedWeapon.transformLeftHandIK.position);
            animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1f);
            //rotate hand ik to how it should be
            animator.SetIKRotation (AvatarIKGoal.LeftHand, theCurrentEquippedWeapon.transformLeftHandIK.rotation);
            animator.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1f);
        }
        else
        {
            //if there none then turn it off
            animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight (AvatarIKGoal.LeftHand, 0f);
        }

        //if weapon has ik hint solver for left elbow
        if (theCurrentEquippedWeapon.transformLeftHandSolver)
        {
            //move hint to where it should be
            animator.SetIKHintPosition (AvatarIKHint.LeftElbow, theCurrentEquippedWeapon.transformLeftHandSolver.position);
            animator.SetIKHintPositionWeight (AvatarIKHint.LeftElbow, 1f);
        }
        else
        {
            //if not then do not use it
            animator.SetIKHintPositionWeight (AvatarIKHint.LeftElbow, 0f);
        }
    }


}
