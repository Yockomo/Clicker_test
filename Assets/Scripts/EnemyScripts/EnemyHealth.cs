using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int clicksToKill;

    public int ClicksToKill { get; private set; }
    public Action<GameObject> OnDieEvent;

    private int currentHealth;

    private void Start()
    {
        ClicksToKill = clicksToKill;
        ResetHealth();
    }

    public void ResetHealth()
    {
        currentHealth = ClicksToKill;
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth < 1)
            Die();
    }

    public void Die()
    {
        OnDieEvent.Invoke(this.gameObject);
    }

    public void ChangeMaxHp(int newHpValue)
    {
        ClicksToKill = newHpValue;
    }
}
