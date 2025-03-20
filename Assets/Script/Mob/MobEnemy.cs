using UnityEngine;
using UnityEngine.AI;

public class MobEnemy : MobRoot
{
    private NavMeshAgent agent;
    private Transform target;
    private Animator animator;

    public float attackRange = 2f;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (GameBase.gameBase.player != null)
            target = GameBase.gameBase.player.transform;
    }

    protected override void FrameMobMove()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                TryAttack();
                animator.SetTrigger("Attack");
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                animator.SetBool("Walk", true);
            }
        }
    }

    private void TryAttack()
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        Debug.Log($"[MobEnemy] {gameObject.name} attacks the player!");
        float damage = DamageReqEvnet();
        GameBase.gameBase.player.DamageResEvnet(damage);
    }
}