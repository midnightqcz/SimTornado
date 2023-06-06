using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesAI : MonoBehaviour
{
    public GameObject Player;

    private void OnEnable()
    {
        Player = GameObject.Find("Tornado_3D");
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) > 950)
        {
            Destroy(this.gameObject);
        }
    }
}
