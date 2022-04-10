using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponPickup : Pickup
{
    //weapon variables
    [SerializeField]
    [Header("Pickup weapon stuff")]
    private Weapon weaponToEquip;

    //enum for type of gun
    public enum WeaponAnimationType
    {
        Rifle = 0,
        Handgun = 1
    }
    [SerializeField]
    private WeaponAnimationType animationType = WeaponAnimationType.Rifle;
    
    //int for easy access
    [SerializeField]
    private int weaponType;

    void Awake()
    {
        //assign variable
        weaponType = (int) animationType;
    }

    //when picked up
    override public void OnPickUp(Player player)
    {
        player.Unequip();
        //destroy the pickup and give the weapon
        player.EquipWeapon(weaponToEquip, weaponType);
        base.OnPickUp(player);
    }
}
