using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrollingState : StateMachineBehaviour
{
    float timer;
    [SerializeField] float patrollingTime = 10f;
    Transform player;
    UnityEngine.AI.NavMeshAgent agent;

    [SerializeField] float detectionAreaRadius = 18f;
    [SerializeField] float patrolSpeed = 2f;
    List<Transform> waypointsList = new List<Transform>();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timer = 0;
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
       agent.speed = patrolSpeed;


        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointCluster.transform)
        {
            waypointsList.Add(t);
        }

        Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        if (agent.enabled) {
            agent.SetDestination(nextPosition);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.enabled && agent.remainingDistance <= agent.stoppingDistance )
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }

        // --- Transition to Idle State ---
        timer += Time.deltaTime;
        if (timer > patrollingTime)
        {
            animator.SetBool("isPatroling", false);
        }


       float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
       if (distanceFromPlayer < detectionAreaRadius) {
           animator.SetBool("isChasing", true);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (agent.enabled) {
            agent.SetDestination(agent.transform.position);
       }
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
