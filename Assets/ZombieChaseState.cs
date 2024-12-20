using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseState : StateMachineBehaviour
{

    NavMeshAgent agent;
    Transform player;

    [SerializeField] float chaseSpeed = 6f;
    [SerializeField] float stopChasingDistance = 40f;
    [SerializeField] float attackingDistance = 1.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<NavMeshAgent>();
       agent.speed = chaseSpeed;
       animator.SetBool("isChasing", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
       if (agent.enabled) {
            agent.SetDestination(player.position);
       }
       animator.transform.LookAt(player);  
       float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

       if (distanceFromPlayer > stopChasingDistance) {
           // animator.SetBool("isChasing", false);
            // agent.isStopped = true;
       }
       if (distanceFromPlayer < attackingDistance) {
           animator.SetBool("isAttacking", true);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
