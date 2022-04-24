using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //refs for the player movement
    public Player player;
    private Vector3 mouseRayPlace;
    [SerializeField]
    private bool playable;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPaused)
        {
            return;
        }

        if(playable)
        {
            if(player != null)
            {
                //move controller to affect the player
                player.Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) );

                //create plane variable and make raycast according to screen perspective
                Plane plane = new Plane (Vector3.up, transform.position);
                Ray mouseRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);

                //float to hold where ray cast hit
                float distance;

                //if the ray cast does hit then send it to the player
                if (plane.Raycast (mouseRaycast, out distance))
                {
                    //get the position of the ray hit in vector 3
                    mouseRayPlace = mouseRaycast.GetPoint(distance);
                    //make player look at it
                    player.LookAtMouse(mouseRayPlace);
                }    
            }    
        }
    }

    private void Uncontroll()
    {
        playable = false;
    }

    public void Control()
    {
        playable = true;
    }

    public void GetPlayer()
    {
        player = GameManager.Instance.thePlayer;
        player.playerHealth.OnDeath.AddListener (Uncontroll);
    }
}
