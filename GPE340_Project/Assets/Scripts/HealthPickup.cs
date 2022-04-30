using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField]
    [Header("Pickup variables")]
    private float healAmmount;
    [SerializeField]
    private AudioClipPlayer clipPlayer;

    override public void OnPickUp(Player player)
    {
        player.playerHealth.Heal(healAmmount);
        Instantiate(clipPlayer, gameObject.transform.position, gameObject.transform.rotation);
        base.OnPickUp(player);
    }
}
