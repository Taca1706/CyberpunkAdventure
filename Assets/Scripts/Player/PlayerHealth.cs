using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text damageText;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        
        if(health <= 0 )
        {
            healthText.text = "0/" + maxHealth;
        }
        else
        {
            healthText.text = health + "/" + maxHealth;
        }
        damageText.text = "Damage: " + damage;
    }

    public void AddHealth(int val)
    {
        if(health + val <= maxHealth)
        {
            health += val;
        }
        else
        {
            health = maxHealth;
        }
    }
    public void AddDamage(int val)
    {  
        damage += val;
    }
}
