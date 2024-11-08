using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    private Weapon weapon;
    private static Weapon currentWeapon;

    void Awake()
    {
        if (weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder);
        }
        else
        {
            Debug.LogWarning("Weapon Holder belum diassign di Inspector.");
        }
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Objek Player Memasuki trigger");
            if (currentWeapon != null && currentWeapon != weapon)
            {
                ReleaseCurrentWeapon();
            }
            currentWeapon = weapon;
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = Vector3.zero;
            TurnVisual(true);
            
        }
        else
        {
            Debug.Log("Bukan Objek Player yang memasuki Trigger");
        }
    }

    void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
        else
        {
            Debug.LogWarning("Weapon tidak ditemukan.");
        }
    }

    void TurnVisual(bool on, Weapon specificWeapon)
    {
        if (specificWeapon != null)
        {
            specificWeapon.gameObject.SetActive(on);
        }
        else
        {
            Debug.LogWarning("Specific weapon tidak ditemukan.");
        }
    }
    void ReleaseCurrentWeapon()
    {
        currentWeapon.transform.SetParent(null);
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = null;
    }
}
