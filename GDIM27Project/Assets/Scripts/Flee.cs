using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : Action
{
    public NavMeshAgent myAgent;

    public SharedTransform target;


    public override void OnStart()
    {
        base.OnStart();
        myAgent = GetComponent<NavMeshAgent>();
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

        Vector3 fleeVector = tornadoObject.transform.position - myGameObject.transform.position;
        myAgent.SetDestination(myGameObject.transform.position - fleeVector);

    }

}
