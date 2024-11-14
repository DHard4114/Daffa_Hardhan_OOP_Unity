using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    public HealthComponent healthComponent;

    private void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public void Damage(int damageAmount)
    {
        if (healthComponent != null)
        {
            healthComponent.Subtract(damageAmount);
        }
    }

    public void Damage(Bullet bullet)
    {
        if (healthComponent != null)
        {
            healthComponent.Subtract(bullet.damage);
        }
    }
}

