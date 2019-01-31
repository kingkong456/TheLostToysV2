using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class enemyController : MonoBehaviour {
    //controller state can move
    public bool is_canMove = false;
    public NavMeshAgent nav;
    public EnemyBehavior my_behavior;
    public Animator animator;
    public Collider attack_Col;

    [Header("Walk Ststus")]
    public float run_speed = 1.2f;
    public float walk_speed = 0.85f;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = walk_speed;
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        attack_Col.enabled = false;
        my_behavior = GetComponent<EnemyBehavior>();
    }

    private void FixedUpdate()
    {
        my_behavior.find_target();

        if (my_behavior.target != null)
        {
            if (Vector3.Distance(transform.position, my_behavior.target.position) <= nav.stoppingDistance)
            {
                //attack
                faceToTarget();
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", false);
                animator.SetTrigger("attack");
                return;
            }
            faceToTarget();
            nav.speed = run_speed;
            animator.SetBool("isRun", true);
            animator.SetBool("isWalk", false);
            nav.SetDestination(my_behavior.target.position);
        }
        else
        {
            nav.speed = walk_speed;
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }
    }

    void faceToTarget()
    {
        Vector3 diraction = (my_behavior.target.position - transform.position).normalized;
        Quaternion lookRotaion = Quaternion.LookRotation(new Vector3(diraction.x, 0, diraction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotaion, Time.deltaTime * 5);
    }

    public void attack()
    {
        attack_Col.enabled = true;
        attack_Col.GetComponent<danger_col>().power = my_behavior.attack_power;
    }

    public void end_attack()
    {
        attack_Col.enabled = false;
    }

    public void setWalk(Transform target_walk)
    {
        nav.SetDestination(target_walk.position);
    }
}
