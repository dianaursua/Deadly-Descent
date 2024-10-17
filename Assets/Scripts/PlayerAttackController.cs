using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private FixedJoystick _attackJoystick;  // Attack Joystick
    [SerializeField] private Transform _firePoint;           // Where the bullets will fire from (on the player or turret)
    [SerializeField] private GameObject _bulletPrefab;       // The bullet prefab to be fired
    [SerializeField] private float _bulletSpeed = 20f;       // Speed of the bullet
    [SerializeField] private float _fireRate = 0.5f;         // Fire rate (time between shots)
    [SerializeField] private float _rotationSpeed = 10f;     // Speed of player rotation towards attack direction

    private bool _isFiring = false;                          // Whether the player is currently firing
    private float _nextFireTime = 0f;                        // Time until the next shot can be fired

    private Vector3 _lastAttackDirection = Vector3.forward;  // Store the last valid attack direction

    public enum WeaponType
    {
        None,
        Gun,
        Axe,
        BaseballBat,
        Punch // Default weapon
    }

    private WeaponType _currentWeapon = WeaponType.Punch; // Start with Punch as the equipped weapon

    private Dictionary<WeaponType, int> weaponDamage = new Dictionary<WeaponType, int>
    {
        { WeaponType.Gun, 10 },            // Example damage for Gun
        { WeaponType.Axe, 15 },            // Damage for Axe
        { WeaponType.BaseballBat, 12 },    // Damage for Baseball Bat
        { WeaponType.Punch, 5 }             // Damage for Punch
    };

    private void Update()
    {
        HandleAttackInput();
    }

    private void HandleAttackInput()
    {
        if (_currentWeapon == WeaponType.None)
        {
            Debug.Log("No weapon equipped. Cannot attack.");
            return; // If no weapon is equipped, exit the function
        }

        // Check if attack joystick is moved (attack direction is not neutral)
        if (Mathf.Abs(_attackJoystick.Horizontal) > 0.1f || Mathf.Abs(_attackJoystick.Vertical) > 0.1f)
        {
            // Calculate the direction of attack based on joystick input
            Vector3 attackDirection = new Vector3(_attackJoystick.Horizontal, 0, _attackJoystick.Vertical);
            _lastAttackDirection = attackDirection;  // Update the last valid attack direction

            Quaternion targetRotation = Quaternion.LookRotation(attackDirection);

            // Smoothly rotate player towards attack direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            // Start firing automatically if the fire rate allows it
            if (!_isFiring)
            {
                StartCoroutine(AutoFire());
            }
        }
        else
        {
            // Stop firing if joystick is not moved
            _isFiring = false;

            // Keep the player facing the last direction when joystick is not moved
            if (_lastAttackDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_lastAttackDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }

    private IEnumerator AutoFire()
    {
        _isFiring = true;

        while (_isFiring)
        {
            if (Time.time >= _nextFireTime)
            {
                Fire();
                _nextFireTime = Time.time + _fireRate;
            }

            yield return null; // Wait for the next frame to check firing condition again
        }
    }

    private void Fire()
    {
        if (_currentWeapon == WeaponType.Gun) // Only fire if the Gun is equipped
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = _firePoint.forward * _bulletSpeed;

            Destroy(bullet, 5f);
        }
        else if (_currentWeapon == WeaponType.Axe || _currentWeapon == WeaponType.BaseballBat)
        {
            // Implement melee damage logic
            Debug.Log("Using Weapon");
            DealMeleeDamage();
        }
        else if (_currentWeapon == WeaponType.Punch)
        {
            Debug.Log("Punching!");
            // Implement Punch logic here, if applicable
            DealMeleeDamage();
        }
    }

    private void DealMeleeDamage()
    {
        // Check for nearby enemies to deal damage
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f); // Adjust the radius as needed

        foreach (var hitCollider in hitColliders)
        {
            EnemyHealth enemy = hitCollider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                int damage = weaponDamage[_currentWeapon]; // Get the damage for the current weapon
                enemy.TakeDamage(damage); // Apply damage to the enemy
                Debug.Log($"{_currentWeapon} hit: {damage} damage dealt.");
            }
        }
    }

    public void EquipWeapon(WeaponType weaponType)
    {
        if (weaponType != WeaponType.Punch)
        {
            _currentWeapon = weaponType; // Equip the specified weapon
            Debug.Log("Equipped weapon: " + weaponType);
        }
    }

    public void ResetWeapon()
    {
        _currentWeapon = WeaponType.None; // Reset to no weapon
        Debug.Log("Weapon reset to none.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for weapon pickups
        if (other.CompareTag("Gun"))
        {
            EquipWeapon(WeaponType.Gun);
            Destroy(other.gameObject); // Destroy the weapon after picking it up
        }
        else if (other.CompareTag("Axe"))
        {
            EquipWeapon(WeaponType.Axe);
            Destroy(other.gameObject); // Destroy the weapon after picking it up
        }
        else if (other.CompareTag("Baseball Bat"))
        {
            EquipWeapon(WeaponType.BaseballBat);
            Destroy(other.gameObject); // Destroy the weapon after picking it up
        }
    }
}




//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//public class PlayerAttackController : MonoBehaviour
//{
//    [SerializeField] private FixedJoystick _attackJoystick;   // Attack Joystick
//    [SerializeField] private Transform _firePoint;            // Where the bullets will fire from (on the player or turret)
//    [SerializeField] private GameObject _bulletPrefab;        // The bullet prefab to be fired
//    [SerializeField] private float _bulletSpeed = 20f;        // Speed of the bullet
//    [SerializeField] private float _rotationSpeed = 10f;      // Speed of player rotation towards attack direction

//    // Weapon properties
//    [SerializeField] private int _maxAmmo = 20;              // Max ammo for gun
//    [SerializeField] private float _gunFireRate = 0.5f;      // Fire rate for gun
//    [SerializeField] private float _axeFireRate = 1.0f;      // Fire rate for axe (melee)
//    [SerializeField] private float _baseballBatFireRate = 1.5f; // Fire rate for baseball bat (melee)

//    private bool _isFiring = false;                           // Whether the player is currently firing
//    private float _nextFireTime = 0f;                         // Time until the next shot can be fired
//    private int _currentAmmo;                                 // Current ammo for the gun

//    private Vector3 _lastAttackDirection = Vector3.forward;   // Store the last valid attack direction

//    public enum WeaponType
//    {
//        None,
//        Gun,
//        Axe,
//        BaseballBat,
//        Punch // Default weapon
//    }

//    private WeaponType _currentWeapon = WeaponType.Punch; // Start with Punch as the equipped weapon

//    private Dictionary<WeaponType, int> weaponDamage = new Dictionary<WeaponType, int>
//    {
//        { WeaponType.Gun, 10 },              // Example damage for Gun
//        { WeaponType.Axe, 15 },              // Damage for Axe
//        { WeaponType.BaseballBat, 12 },      // Damage for Baseball Bat
//        { WeaponType.Punch, 5 }               // Damage for Punch
//    };

//    private Dictionary<WeaponType, float> weaponFireRates = new Dictionary<WeaponType, float>
//    {
//        { WeaponType.Gun, 0.5f },             // Fire rate for Gun
//        { WeaponType.Axe, 1.0f },             // Fire rate for Axe
//        { WeaponType.BaseballBat, 1.5f },     // Fire rate for Baseball Bat
//        { WeaponType.Punch, 1.0f }             // Fire rate for Punch
//    };

//    private void Start()
//    {
//        // Initialize current ammo to max ammo for the gun
//        _currentAmmo = _maxAmmo;
//    }

//    private void Update()
//    {
//        HandleAttackInput();
//    }

//    private void HandleAttackInput()
//    {
//        if (_currentWeapon == WeaponType.None)
//        {
//            Debug.Log("No weapon equipped. Cannot attack.");
//            return; // If no weapon is equipped, exit the function
//        }

//        // Check if attack joystick is moved (attack direction is not neutral)
//        if (Mathf.Abs(_attackJoystick.Horizontal) > 0.1f || Mathf.Abs(_attackJoystick.Vertical) > 0.1f)
//        {
//            // Calculate the direction of attack based on joystick input
//            Vector3 attackDirection = new Vector3(_attackJoystick.Horizontal, 0, _attackJoystick.Vertical);
//            _lastAttackDirection = attackDirection;  // Update the last valid attack direction

//            Quaternion targetRotation = Quaternion.LookRotation(attackDirection);

//            // Smoothly rotate player towards attack direction
//            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

//            // Start firing automatically if the fire rate allows it
//            if (!_isFiring)
//            {
//                StartCoroutine(AutoFire());
//            }
//        }
//        else
//        {
//            // Stop firing if joystick is not moved
//            _isFiring = false;

//            // Keep the player facing the last direction when joystick is not moved
//            if (_lastAttackDirection != Vector3.zero)
//            {
//                Quaternion targetRotation = Quaternion.LookRotation(_lastAttackDirection);
//                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
//            }
//        }
//    }

//    private IEnumerator AutoFire()
//    {
//        _isFiring = true;

//        while (_isFiring)
//        {
//            if (Time.time >= _nextFireTime)
//            {
//                Fire();
//                _nextFireTime = Time.time + weaponFireRates[_currentWeapon]; // Use the fire rate of the current weapon
//            }

//            yield return null; // Wait for the next frame to check firing condition again
//        }
//    }

//    private void Fire()
//    {
//        if (_currentWeapon == WeaponType.Gun && _currentAmmo > 0) // Only fire if the Gun is equipped and there are bullets
//        {
//            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
//            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
//            bulletRb.velocity = _firePoint.forward * _bulletSpeed;

//            Destroy(bullet, 5f);
//            _currentAmmo--;

//            // Destroy the gun if ammo runs out
//            if (_currentAmmo <= 0)
//            {
//                Debug.Log("Gun is out of ammo and will be destroyed.");
//                ResetWeapon(); // Reset to no weapon
//            }
//        }
//        else if (_currentWeapon == WeaponType.Axe || _currentWeapon == WeaponType.BaseballBat)
//        {
//            // Implement melee damage logic and breakable weapons
//            Debug.Log("Using Weapon");
//            DealMeleeDamage();
//            BreakMeleeWeapon();
//        }
//        else if (_currentWeapon == WeaponType.Punch)
//        {
//            Debug.Log("Punching!");
//            // Implement Punch logic here, if applicable
//            DealMeleeDamage();
//        }
//    }

//    private void DealMeleeDamage()
//    {
//        // Check for nearby enemies to deal damage
//        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f); // Adjust the radius as needed

//        foreach (var hitCollider in hitColliders)
//        {
//            EnemyHealth enemy = hitCollider.GetComponent<EnemyHealth>();
//            if (enemy != null)
//            {
//                int damage = weaponDamage[_currentWeapon]; // Get the damage for the current weapon
//                enemy.TakeDamage(damage); // Apply damage to the enemy
//                Debug.Log($"{_currentWeapon} hit: {damage} damage dealt.");
//            }
//        }
//    }

//    private void BreakMeleeWeapon()
//    {
//        // Logic to break melee weapon after use
//        if (_currentWeapon == WeaponType.Axe || _currentWeapon == WeaponType.BaseballBat)
//        {
//            Debug.Log($"{_currentWeapon} is broken and will be reset.");
//            ResetWeapon(); // Reset to no weapon
//        }
//    }

//    public void EquipWeapon(WeaponType weaponType)
//    {
//        if (weaponType == WeaponType.Gun)
//        {
//            _currentAmmo = _maxAmmo; // Reset ammo when equipping the gun
//        }

//        _currentWeapon = weaponType; // Equip the specified weapon
//        Debug.Log("Equipped weapon: " + weaponType);
//    }

//    public void ResetWeapon()
//    {
//        _currentWeapon = WeaponType.None; // Reset to no weapon
//        Debug.Log("Weapon reset to none.");
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // Check for weapon pickups
//        if (other.CompareTag("Gun"))
//        {
//            EquipWeapon(WeaponType.Gun);
//            Destroy(other.gameObject); // Destroy the weapon after picking it up
//        }
//        else if (other.CompareTag("Axe"))
//        {
//            EquipWeapon(WeaponType.Axe);
//            Destroy(other.gameObject); // Destroy the weapon after picking it up
//        }
//        else if (other.CompareTag("Baseball Bat"))
//        {
//            EquipWeapon(WeaponType.BaseballBat);
//            Destroy(other.gameObject); // Destroy the weapon after picking it up
//        }
//    }
//}
