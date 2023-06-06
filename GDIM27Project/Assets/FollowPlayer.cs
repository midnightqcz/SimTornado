using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // 目标对象，你希望跟随的对象
    public Vector3 offset; // 跟随的位置偏移

    void Update()
    {      
        if(Vector3.Distance(transform.position, target.position) > 50)
        {
            //transform.position = target.position + offset;
            transform.position = new Vector3(target.position.x + offset.x, 0, target.position.z + offset.z);
        }
    }
}
