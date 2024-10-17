//using UnityEngine;

//public class WeaponPickupButton : MonoBehaviour
//{
//    public PlayerAttackController playerAttackController; // Reference to the PlayerAttackController

//    public void OnPickupGun()
//    {
//        playerAttackController.EquipWeapon(PlayerAttackController.WeaponType.Gun);
//    }

//    public void OnPickupAxe()
//    {
//        playerAttackController.EquipWeapon(PlayerAttackController.WeaponType.Axe);
//    }

//    public void OnPickupBaseballBat()
//    {
//        playerAttackController.EquipWeapon(PlayerAttackController.WeaponType.BaseballBat);
//    }
//}
using UnityEngine;

public class WeaponPickupButton : MonoBehaviour
{
    public WeaponData weaponData; // Reference to the weapon data
}

