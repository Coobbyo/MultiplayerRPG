using UnityEngine;
using Unity.Netcode;

public class CharacterStatsNetwork : NetworkBehaviour
{
    public Stat maxHealth;
    public int currentHealth {get; protected set;}

    public Stat damage;
    public Stat armor;

    public event System.Action OnHealthReachedZero;

    public virtual void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void Start() {}

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if(currentHealth <= 0)
        {
            if(OnHealthReachedZero != null)
            {
                OnHealthReachedZero();
            }
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
    }
}
