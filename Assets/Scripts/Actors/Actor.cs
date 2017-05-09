using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {
    [Header("Health")]
    public int baseHealth;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    public virtual void Init() {
        MaxHealth = CurrentHealth = baseHealth;
    }

    public virtual void AlterHealth(int health) {
        CurrentHealth += health;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        if (CurrentHealth <= 0)
            Die();
    }

    protected virtual void Die() {
        Debug.Log("Actually need to fill this in");
    }
}
