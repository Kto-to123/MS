using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigations : MonoBehaviour
{
    NavMeshAgent myAgent;
    public GameObject player;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        //myAgent.destination = player.transform.position;
    }

    public void SetDestantion(Transform _transform)
    {
        if (myAgent.isActiveAndEnabled)
        {
            myAgent.destination = _transform.position;
        }
    }
}
