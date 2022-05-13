using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10f;
    public int rand;
    public Animator animator;

    public void TakeDamage(float damage)
    {
        animator.SetBool("isHurtedZombi", true);
        health -= damage;
        if(health <= 0f)
        {
            Dead();
        }

        
    }

    private void Dead()
    {
        animator.SetBool("isDyingZombi", true);
        Destroy(gameObject, 3f);
    }


}
