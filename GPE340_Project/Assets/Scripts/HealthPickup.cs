using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField]
    [Header("Pickup variables")]
    private float healAmmount;

    override public void OnPickUp(Player player)
    {
        player.playerHealth.Heal(healAmmount);
        base.OnPickUp(player);
    }
}
