using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables to use  
    [SerializeField]  
    //player speed
    private float speed = 4f; 
    //ref for the animator
    private Animator animator;

    private void Awake()
    {
        //obtain the animator component at the very start before first frame is drawn
        animator = GetComponent<Animator>();
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
        speed = 6f;
    }
}
