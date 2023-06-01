using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WithinSight : Conditional
{
    public Collider myCollider;
    
    //found a target than share to Bhavior Tree parameter
    public SharedGameObject target;
    public SharedTransform targetTransform;
    public SharedVector3 targetPos;

    public string playerTag;
    public SharedFloat sightRange;


    public override void OnAwake()
    {
        base.OnAwake();
        myCollider = GetComponent<Collider>();
        target = GameObject.FindGameObjectWithTag(playerTag);
    }

    public override TaskStatus OnUpdate()
    {
        //Keep checking the position of player
        targetTransform.Value = target.Value.transform;
        targetPos.Value = targetTransform.Value.position;

        // if sight Range has player return Task Status.Success
        if (withinSight(myCollider, sightRange.Value, targetPos.Value)) 
        {
            //Debug.Log("Find Target" + targetPos.Value);

            return TaskStatus.Success;
        }
        //if did not found keep searching next frame        
        else 
        {
            //Debug.Log("do not Find Target" + targetPos.Value); 
            return TaskStatus.Running; 
        }
    }

    //Check Player is in the sight range
    public bool withinSight(Collider myCollider, float sightRange, Vector3 targetPos)
    {

       if(Vector3.Distance(myCollider.transform.position, targetPos) < sightRange)
        {
            return true;
        }
        else { return false; }
    }
}
