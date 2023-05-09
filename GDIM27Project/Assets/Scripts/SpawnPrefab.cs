using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPrefab : MonoBehaviour
{
    //gameobject for spawning
    public List<GameObject> prefabList = new List<GameObject>();

    //set range for position
    /*
     * public float MinX = ?;
     * public float MinY = ?;
     * public float MaxX = ?;
     * public float MaxY = ?;
     * public float MinZ = ?;
     * public float MaxZ = ?;
     */

    void Start()
    {
        /*
        float x = Random.Range(MinX, MaxX);
        float y = Random.Range(MinY, MaxY);
        float y = Random.Range(MinZ, MaxZ);
        */
        //create a for loop to make it spawn for multiple times if needed.
        int prefabIndex = UnityEngine.Random.Range(0, prefabList.Count - 1);
        //replace with xyz after finding out the range
        Instantiate(prefabList[prefabIndex],new Vector3(0,0,0), Quaternion.identity);
    }


}
