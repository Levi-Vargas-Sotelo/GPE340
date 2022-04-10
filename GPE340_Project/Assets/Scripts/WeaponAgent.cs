using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAgent : MonoBehaviour
{
    [Header("Weapon Attributes")]
    [SerializeField] 
    private Transform weaponPoint;
    [SerializeField] 
    public Weapon equippedWeapon;

    public Vector3 dropOffset;
    public Weapon testgun;


    void Awake()
    {
        EquipWeapon(testgun, 0);
    }
    //equip weapon
    public virtual void EquipWeapon(Weapon weapon, int gunType)
    {
        equippedWeapon = Instantiate (weapon) as Weapon; 
        equippedWeapon.transform.SetParent (weaponPoint);
        equippedWeapon.transform.localPosition = weapon.transform.localPosition;
        equippedWeapon.transform.localRotation = weapon.transform.localRotation;
        equippedWeapon.gameObject.layer = gameObject.layer;
    }

    //unequip it
    public virtual void Unequip()
    {
        if (equippedWeapon)
        {
            DropGun(equippedWeapon.thisGunPrefab);
            Destroy (equippedWeapon.gameObject);
            equippedWeapon = null;
        }
    }

    public virtual void DropGun(Pickup theWeapon)
    {
        Pickup gunDropped = Instantiate(theWeapon) as Pickup;
        gunDropped.transform.position = this.transform.position + dropOffset;
    }
}
