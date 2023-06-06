using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FarFromTarget : Conditional
{
    public Collider myCollider;

    //found a target than share to Bhavior Tree parameter
    public SharedGameObject target;
    public SharedTransform targetTransform;
    public SharedVector3 targetPos;



    public string playerTag;
    public SharedFloat sightRange;

    private AudioSource fleeSource;


    public override void OnAwake()
    {
        base.OnAwake();
        myCollider = GetComponent<Collider>();
        fleeSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag(playerTag);
    }

    public override TaskStatus OnUpdate()
    {
        //Keep checking the position of player
        targetTransform.Value = target.Value.transform;
        targetPos.Value = targetTransform.Value.position;

        // if sight Range has player return Task Status.Success
        if (!withinSight(myCollider, sightRange.Value, targetPos.Value))
        {
            //Debug.Log("far from target" + targetPos.Value);
            //fleeSource.Stop();
            return TaskStatus.Failure;

        }
        //if did not found keep searching next frame        
        else
        {
            //Debug.Log("still in range" + targetPos.Value);
            return TaskStatus.Running;
        }
    }

    //Check Player is in the sight range
    public bool withinSight(Collider myCollider, float sightRange, Vector3 targetPos)
    {
        if (Vector3.Distance(myCollider.transform.position, targetPos) < sightRange + 2f)
        {
            return true;
        }
        else { return false; }
    }
}
