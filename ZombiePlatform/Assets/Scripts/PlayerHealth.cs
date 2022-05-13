using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Range(0f,100f)]
    public float health = 100f;
    public ProgressBar healthBar;

    private void Start()
    {
        SetHealth();
    }

    private void Update()
    {
        SetHealth();
    }

    public void TakeDamage(float damage)
    {
        ThirdPersonMovement anim = this.GetComponent<ThirdPersonMovement>();
        anim.animator.SetBool("IsHurted", true);

        health -= damage;

        if(health <= 0)
            Dead();

    }

    public void Dead()
    {
        PlayerPrefs.SetInt("win", 0);
        SceneManager.LoadScene(2);
    }

    public void SetHealth()
    {
        healthBar.BarValue = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Life")
        {
            health = 100f;
            Destroy(other.gameObject);
        }
        if(other.tag == "Ammo")
        {
            this.GetComponent<ThirdPersonMovement>().Reload();
            Destroy(other.gameObject);
        }

        if(other.tag == "EndGameWin")
        {
            PlayerPrefs.SetInt("win", 1);
            SceneManager.LoadScene(2);
        }
    }

}
