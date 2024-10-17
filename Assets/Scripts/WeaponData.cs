using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;        // Name of the weapon
    public PlayerAttackController.WeaponType weaponType; // Type of weapon
    public int damage;               // Damage dealt by the weapon
    public float fireRate;           // Rate of fire
    public float range;              // Effective range of the weapon
    public GameObject bulletPrefab;  // Bullet prefab (only for ranged weapons)
}
