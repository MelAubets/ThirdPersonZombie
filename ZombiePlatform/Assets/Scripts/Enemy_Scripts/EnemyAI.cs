using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
        Attack,
        Dead,
    }
    private Vector3 startingPosition;
    public float walkRadius;

    public NavMeshAgent agent;
    public GameObject target;

    public float range = 50f;
    public float damage = 2f;
    public float speed = 1f;
    private float nextShootTime;

    public Animator animator;

    public FieldOfView fow;

    private State state;

    private void Awake()
    {
        state = State.Roaming;
    }
    private void Start()
    {
        startingPosition = transform.position;
        StartCoroutine(NotHurted());
    }
    private void Update()
    {
        EnemyHealth enHealth = agent.GetComponent<EnemyHealth>();
        switch (state)
        {
            default:
            case State.Roaming:
                animator.SetBool("isAttacking", false);
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(GetRoamingPosition());
                    agent.speed = speed;
                }

                if (FindTarget())
                    state = State.ChaseTarget;
                if (enHealth.health <= 0)
                    state = State.Dead;
                break;
            case State.ChaseTarget:
                if (!FindTarget())
                    state = State.Roaming;
                else
                {
                    Vector3 lastPosition = fow.visibleTargets[0].position;
                    agent.SetDestination(lastPosition);
                    agent.speed = speed;

                    if (Vector3.Distance(agent.transform.position, target.transform.position) <= 0.5f)
                        state = State.Attack;
                }
                if (enHealth.health <= 0)
                    state = State.Dead;
                break;
            case State.Attack:
                if (Time.time > nextShootTime)
                {
                    agent.SetDestination(agent.transform.position);
                    agent.speed = 0f;
                    transform.LookAt(target.transform);
                    Attack();
                    float attackRate = 0.5f;
                    nextShootTime = Time.time + attackRate;
                }
                if (Vector3.Distance(agent.transform.position, target.transform.position) >= 0.6f)
                    state = State.Roaming;
                if (!FindTarget())
                    state = State.Roaming;
                if (enHealth.health <= 0)
                    state = State.Dead;
                break;
            case State.Dead:
                agent.SetDestination(agent.transform.position);
                agent.speed = 0f;
                break;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private bool FindTarget()
    {
        if (fow.visibleTargets.Count != 0)
        {
            return true;
        }

        return false;
    }

    private void Attack()
    {
        animator.SetBool("isAttacking", true);
        PlayerHealth player = target.GetComponent<PlayerHealth>();
        if (player != null)
            player.TakeDamage(damage);
    }

    IEnumerator NotHurted()
    {
        while (true)
        {
            animator.SetBool("isHurtedZombi", false);
            yield return new WaitForSeconds(1f);
        }
        
    }

}
