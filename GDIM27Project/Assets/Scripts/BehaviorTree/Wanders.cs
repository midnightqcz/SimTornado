using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Wanders : Action
{

    public NavMeshAgent myAgent;

    public float minWanderDuration = 2.0f; // The minimum duration of the wander behavior
    public float maxWanderDuration = 5.0f; // The maximum duration of the wander behavior
    private float wanderDuration; // The actual duration of the wander behavior
    private float wanderStartTime;
    [Header("Wander")]
    [HideInInspector] public Vector3 startWanderPosition;
    Vector3 wanderTarget = Vector3.zero;
    [SerializeField] private float wanderRadius = 5;
    [SerializeField] private float wanderDistance = 10;
    [SerializeField] private float wanderJitter = 1;


    public override void OnAwake()
    {
        myAgent = GetComponent<NavMeshAgent>();
        startWanderPosition = this.gameObject.transform.position;
    }
    public override void OnStart()
    {
        wanderStartTime = Time.time; // Record the start time of the wander behavior
        wanderDuration = Random.Range(minWanderDuration, maxWanderDuration);
        myAgent.isStopped = false;
        Wander(this.gameObject, myAgent, startWanderPosition);
    }

    public override TaskStatus OnUpdate()
    {
       
        // If the wander duration has passed, return a success status to indicate the task is finished
        if (Time.time - wanderStartTime > wanderDuration)
        {
            return TaskStatus.Success;
        }

        // Otherwise, return a running status to indicate the task is still in progress
        return TaskStatus.Running;
    }


    public void Wander(GameObject myGameObject, NavMeshAgent myAgent, Vector3 startPosition)
    {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                        0,
                                        Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget;
        Vector3 targetWorld = myGameObject.transform.InverseTransformVector(targetLocal);
        targetWorld += startPosition;

        // Check if targetWorld is within the patrol radius
        if (Vector3.Distance(startPosition, targetWorld) > wanderRadius || Vector3.Distance(startPosition, targetWorld) < 1f)
        {
            // If the targetWorld is outside the patrol radius or too close to the startPosition, regenerate wanderTarget
            wanderTarget = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            // Recalculate targetWorld
            targetLocal = wanderTarget;
            targetWorld = myGameObject.transform.InverseTransformVector(targetLocal);
            targetWorld += startPosition;
        }

        myAgent.SetDestination(targetWorld);
    }


}
