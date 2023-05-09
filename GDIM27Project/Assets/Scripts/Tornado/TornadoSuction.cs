using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TornadoSuction : MonoBehaviour
{
    /*
     *  small, medium, large variable changed, allow more building type
     *  all type changed to float, due to there are a lot pieces in a building, trynna make number smaller to fit)
     */

    CapsuleCollider collider;
    [SerializeField] int CameraDisScale;
    [SerializeField] private CinemachineVirtualCamera vcam;


    private GameObject comp;


    public float force = 5f; // The strength of the suction force
    public float radius = 5f; // The range within which objects can be sucked in
    // upgrading
    public int points; //points to tracking players point and tornados level.
    [HideInInspector] public int lastLevelPoints = 0; // last time level up
    public int levelUpNeed = 100; // each time level up need
    public float levelControlRate = 0.7f; // The rate control for each time level up need after first time
    [HideInInspector] public int tornadoLevel = 0; // tornado initial level
    [HideInInspector] public int currentPoints = 0; // tracking points of tornado in the game
    public int[] objectPoints = new int[] { 10, 20, 30, 40, 50 }; // lv1,2,3,4,5 object's value
    public float levelUpForce = 0.5f; //level up force adjust
    public float levelUpRadius = 0.5f; //level up radius adjust
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        //Debug.Log(collider.radius);
        comp = GameObject.FindWithTag("vcam1");
    }


    private void OnTriggerStay(Collider other)
    {
        int objectLevel;
        objectLevel = returnTagLevel(other);
        if (tornadoLevel >= objectLevel)
        {
            points = objectPoints[objectLevel];
            moveTowardsTornado(other, points);
            //Debug.Log(currentPoints);
            /*if (other.gameObject.CompareTag("LV1"))
            {
                points = lv_1;
                moveTowardsTornado(other, points);
                Debug.Log(currentPoints);
            }*/
        }
        
    }
    private int returnTagLevel(Collider other)
    {
        if (other.gameObject.CompareTag("LV1"))
        {
            return 0;
        }
        if (other.gameObject.CompareTag("LV2"))
        {
            return 1;
        }
        if (other.gameObject.CompareTag("LV3"))
        {
            return 2;
        }
        if (other.gameObject.CompareTag("LV4"))
        {
            return 3;
        }
        if (other.gameObject.CompareTag("LV5"))
        {
            return 4;
        }
        return 10000;
    }
    private void moveTowardsTornado(Collider other, int points)
    {
        Vector3 direction = transform.position - other.transform.position;
        float distance = direction.magnitude;
        // Check if the other collider is within the suction range of the tornado
        if (distance <= radius)
        {
            // Calculate the strength of the suction force based on the distance between the tornado and the other collider
            float magnitude = force * (1 - distance / radius);

            // Apply the suction force to the other collider in the direction of the tornado
            other.transform.position += direction.normalized * magnitude * Time.deltaTime;

            // If the other collider is close enough to the tornado, destroy it
            if (distance <= 0.5f)
            {
                Destroy(other.gameObject);
                addPoints(points);
            }
        }
    }
    private void addPoints(int points)
    {
        currentPoints += points;
        checkingLevelUp();
    }
    private void checkingLevelUp()
    {
        if (currentPoints >= lastLevelPoints + levelUpNeed)
        {
            levelUp();
            lastLevelPoints = currentPoints;
            levelUpNeed = (int)(levelUpNeed * tornadoLevel * levelControlRate);
            //Debug.Log(tornadoLevel);
        }

    }

    private void levelUp()
    {
        tornadoLevel++;
        changeForce(cForce() + tornadoLevel * levelUpForce);
        changeRadius(cRadius() + tornadoLevel * levelUpRadius);
        this.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        comp.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance += radius / CameraDisScale / 5;
        //Debug.Log(force+ " " + radius);
    }
    public float cForce()
    {
        return force;
    }
    public void changeForce(float nForce)
    {
        force = nForce;
    }
    public float cRadius()
    {
        return radius;
    }
    public void changeRadius(float nRadius)
    {
        radius = nRadius;
    }
}  