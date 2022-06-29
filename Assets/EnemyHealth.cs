using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int clicksToKill;

    private int currentHealth;

    public int ClicksToKill { get; private set; }

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
        if (currentHealth < 0)
            FucnWhenDie();
    }

    public void FucnWhenDie()
    {

    }

    public void ChangeMaxHp(int newHpValue)
    {
        ClicksToKill = newHpValue;
    }
}
