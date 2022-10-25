using UnityEngine;
using Unity.Netcode;

public class CharacterStatsNetwork : NetworkBehaviour
{
    [SerializeField] private Stat maxHealth;
    public int currentHealth {get; protected set;}

    public Stat damage;
    [SerializeField] private Stat armor;

    public bool HasDied
    {
        get
        {
            return currentHealth <= 0;
        }
        set
        {
            currentHealth = value ? 0 : maxHealth.GetValue();
        }
    }

    public event System.Action OnHealthReachedZero;

    public virtual void Awake()
    {
        HasDied = false;
    }

    public virtual void Start(){}

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if(HasDied)
        {
            OnHealthReachedZero?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
    }
}
