using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;
    public Animator anim;
    public GameObject GameOverPanel;
    public HeartBar healthBar; 

    private void Start()
    {
        anim = GetComponent<Animator>();
        GameOverPanel.SetActive(false);

        if (healthBar != null)
        {
            healthBar.SetMaxHealth((int)maxHealth);
            healthBar.SetHealth((int)health);
        }
    }

    public bool isDead = false;

    public void TakeDamage(float damage)
    {
        health = health - damage;

        if (health < 0)
            health = 0;

        if (health > maxHealth)
            health = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetHealth((int)health); 
        }

        if (health == 0)
        {
            isDead = true;
            anim.SetTrigger("IsDead");
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("Game Over!");
        GameOverPanel.SetActive(true);
        anim.SetTrigger("IsDead");
        Time.timeScale = 0f; 
    }
}