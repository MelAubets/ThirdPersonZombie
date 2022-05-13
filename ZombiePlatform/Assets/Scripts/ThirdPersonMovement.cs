using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    public PlayerHealth health;
    public Animator animator;
    public Transform gun;
    public Transform firePoint;

    public int damage;
    public float timeBetweenShooting, range, reloadTime;
    public int magazineSize;
    int bulletsLeft;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public ParticleSystem muzzleFlash, impactEffect;
    public AudioSource shootSound, reloadSound;
    public Text ammo;

    private int speed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        this.GetComponent<Rigidbody>();
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool shoot = Input.GetButton("Fire1");
        bool jump = Input.GetButton("Jump");
        bool run = Input.GetButton("Run");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                animator.SetBool(parameter.name, false);
        }

        if (jump)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(0f, 5f, 0f);
            animator.SetBool("IsJumping", true);
        }

        if (readyToShoot && shoot && !reloading && bulletsLeft > 0)
        {
            animator.SetBool("IsShooting", true);
            Shoot();
        }

        if (direction.magnitude != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            speed = 3;
            animator.SetBool("IsWalking", true);
            if (run)
            {
                speed = 6;
                animator.SetBool("IsRunning", true);
            }
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            GetComponent<Rigidbody>().velocity = (direction * speed);

        }

        if (ammo != null)
            ammo.text = bulletsLeft + " / " + magazineSize;

        if (health.health <= 0)
            animator.SetBool("IsDying", true);

    }

    private void Shoot()
    {
        readyToShoot = false;

        shootSound.Play();
        GameObject instanceMuz = Instantiate(muzzleFlash, firePoint.position, Quaternion.LookRotation(gun.forward)).gameObject;
        Destroy(instanceMuz, 0.1f);

        int enemyMask = LayerMask.GetMask("Enemy");
        Collider[] enemyInShootRadius = Physics.OverlapSphere(transform.position, range, enemyMask);

        for (int i = 0; i < enemyInShootRadius.Length; i++)
        {
            Transform target = enemyInShootRadius[i].transform;
            Debug.Log(enemyInShootRadius[i].name);
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < 60f / 2)
            {
                EnemyHealth enemy = enemyInShootRadius[i].GetComponentInParent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                GameObject instance = Instantiate(impactEffect, enemyInShootRadius[i].transform.position, Quaternion.LookRotation(enemyInShootRadius[i].transform.forward)).gameObject;
                Destroy(instance, 0.1f);
            }

            
        }

        bulletsLeft--;

        Invoke("ResetShot", timeBetweenShooting);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    public void Reload()
    {
        reloading = true;
        reloadSound.Play();
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }


}
