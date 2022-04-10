using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : Pickup
{
    [SerializeField]
    [Header("Pickup variables")]
    private float damage;

    override public void OnPickUp(Player player)
    {
        player.playerHealth.Damage(damage);
        base.OnPickUp(player);
    }
}
