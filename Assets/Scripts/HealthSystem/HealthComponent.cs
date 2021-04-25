using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// most entities should have this - process hit should be called by the bullet which impacts this.
public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    private float StartingHP;
    private float CurrentHP;
    bool dead = false;
    private void Awake()
    {
        CurrentHP = StartingHP;
        OnCurrentHealthChanged?.Invoke(CurrentHP / StartingHP);
    }

    public void ProcessHit(in float damage)
    {
        CurrentHP -= damage;

        OnCurrentHealthChanged?.Invoke(CurrentHP/StartingHP);
        OnCurrentHealthReduced?.Invoke();

        if (CurrentHP <= 0 && !dead)
        {
            gameObject.layer = 14;

            dead = true;
            OnObjectDied?.Invoke();
        }
    }

    public void Heal()
	{
        CurrentHP = StartingHP;
        OnCurrentHealthChanged?.Invoke(CurrentHP / StartingHP);
    }

    public void Die()
    {
        CurrentHP = -1f;
    }

    public event Action OnCurrentHealthReduced;

    public event Action OnObjectDied;
    public event Action<float> OnCurrentHealthChanged;
}
