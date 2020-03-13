using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int affiliation = 0;
    public bool friendlyFire = false;
    public bool destroyOnDeath = true;

    [Tooltip("Callbacks called when health falls below zero")]
    public UnityEvent onDeath;
    [Tooltip("Callbacks called when receiveing damage")]
    public UnityEvent<int> onDamage;
    [Tooltip("Callbacks called when being healed")]
    public UnityEvent<int> onHeal;

    public bool alive = true;

    public void DealDamage(int damage, int affiliation)
    {
        if (friendlyFire || affiliation != this.affiliation)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (onDamage != null)
                onDamage.Invoke(damage);
            if (currentHealth <= 0)
            {
                alive = false;
                if (onDeath != null)
                    onDeath.Invoke();
            }
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (onHeal != null)
            onHeal.Invoke(amount);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!alive)
        {
            Destroy(gameObject);
        }
    }
}
