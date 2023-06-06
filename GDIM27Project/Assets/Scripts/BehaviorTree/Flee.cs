using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Flee : Action
{
    public NavMeshAgent myAgent;
    public SharedTransform target;

    private AudioSource fleeSource; // The sound to play when the object starts fleeing


    public override void OnStart()
    {
        base.OnStart();
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.isStopped = false;
        fleeSource = GetComponent<AudioSource>();
        fleeSource.loop = true;
        //fleeSource.Play();
    }
    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {       
        if(target != null )
        {
            Escape(this.gameObject, target.Value.gameObject);

            return TaskStatus.Running;
        }

        return TaskStatus.Running;
    }

    public void Escape (GameObject myGameObject, GameObject tornadoObject)
    {

        Vector3 fleeVector = myGameObject.transform.position - tornadoObject.transform.position;
        fleeVector.Normalize();
        fleeVector *= myAgent.speed; // Assume the flee distance is equal to the agent's speed

        Vector3 destination = myGameObject.transform.position + fleeVector;

        // Ensure the destination is on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas))
        {
            destination = hit.position;
        }

        myAgent.SetDestination(destination);
    }

}
