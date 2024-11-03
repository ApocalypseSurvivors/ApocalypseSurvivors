using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentScript : MonoBehaviour
{
    public Transform player; 
    // Start is called before the first frame update
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }
}
