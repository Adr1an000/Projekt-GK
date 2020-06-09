//author: Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int affiliation = 0;
    public bool friendlyFire = false;
    public bool destroyOnDeath = true;
    public bool invincible = false;
    public Slider healthBar;

    [Tooltip("Callbacks called when health falls below zero")]
    public UnityEvent onDeath;
    [Tooltip("Callbacks called when receiveing damage")]
    public UnityEvent<int> onDamage;
    [Tooltip("Callbacks called when being healed")]
    public UnityEvent<int> onHeal;

    public bool alive = true;

    public void DealDamage(int damage, int affiliation)
    {
        if ((friendlyFire || affiliation != this.affiliation) && !invincible)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            updateHealthBar();
            if (onDamage != null)
                onDamage.Invoke(damage);
            if (currentHealth <= 0)
            {
                if (onDeath != null)
                    onDeath.Invoke();
                //check if enemy was killed and update stats
                if (this.tag != "Player" && alive==true)
                {
                    GameObject.Find("Stats").GetComponent<Stats>().increment_kills();
                }

                alive = false;
            }
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (onHeal != null)
            onHeal.Invoke(amount);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        updateHealthBar();
    }
    void updateHealthBar()
    {
        if (healthBar)
        {
            healthBar.value = (float) currentHealth / maxHealth;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(!alive && destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }

}
