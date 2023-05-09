using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Wanders : Action
{

    public NavMeshAgent myAgent;

    [Header("Wander")]
    [HideInInspector] public Vector3 startWanderPosition;
    Vector3 wanderTarget = Vector3.zero;
    [SerializeField] private float wanderRadius = 5;
    [SerializeField] private float wanderDistance = 10;
    [SerializeField] private float wanderJitter = 1;


    public override void OnStart()
    {
        myAgent = GetComponent<NavMeshAgent>();
        startWanderPosition = this.gameObject.transform.position;
    }

    public override TaskStatus OnUpdate()
    {
    Wander(this.gameObject, myAgent, startWanderPosition);
    return TaskStatus.Running;
    }


    public void Wander(GameObject myGameObject, NavMeshAgent myAgent, Vector3 startPosition)
    {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                        0,
                                        Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = myGameObject.transform.InverseTransformVector(targetLocal);
        targetWorld += startPosition;
        myAgent.SetDestination(targetWorld);
    }
}
