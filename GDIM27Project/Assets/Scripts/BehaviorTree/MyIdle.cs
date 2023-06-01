using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class MyIdle : Action
{
    public NavMeshAgent myAgent;
    public float idleDuration = 1.0f; // The duration of the idle behavior
    private float idleStartTime;

    public override void OnStart()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.isStopped = true; // Stop the agent
        idleStartTime = Time.time; // Record the start time of the idle behavior
    }

    public override TaskStatus OnUpdate()
    {
        // If the idle duration has passed, return a success status to indicate the task is finished
        if (Time.time - idleStartTime > idleDuration)
        {
            myAgent.isStopped = false; // Allow the agent to move again
            return TaskStatus.Success;
        }

        // Otherwise, return a running status to indicate the task is still in progress
        return TaskStatus.Running;
    }
}