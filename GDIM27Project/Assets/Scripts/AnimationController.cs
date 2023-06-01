using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class AnimationController : MonoBehaviour
{
    private NavMeshAgent myAgent;
    private float speed;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = myAgent.velocity.magnitude;
        anim.SetFloat("speed", speed);
    }
}
