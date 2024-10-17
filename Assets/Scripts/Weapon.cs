// Weapon.cs
using UnityEngine;

[System.Serializable] // Allows the class to be serialized in the Inspector
public class Weapon
{
    public WeaponType weaponType; // The type of weapon
    public string weaponName;      // Name of the weapon
    public float damage;           // Damage dealt by the weapon
    public float fireRate;         // Rate of fire
    public float range;            // Effective range of the weapon
}
