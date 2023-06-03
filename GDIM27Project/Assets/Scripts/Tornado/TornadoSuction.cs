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
    public int middleLevelAudio = 2; //deternmine when to change the soundeffect of the tornado to middle
    public int largeLevelAudio = 3; //deternmine when to change the soundeffect of the tornado to large
    public float levelUpScaleIncrease =0.01f; //increasing the size of the tornado in a certain ration
    public float destroyArea = 0.5f;

    AudioManager TornadoAudioManager = new AudioManager();
    public GameObject AudioObject;

    public delegate void OnLevelUpAction(int newLevel);
    public static event OnLevelUpAction OnLevelUp;

    private float currentCam;
    public GameObject speedLine; // componet of speed line
    
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        //Debug.Log(collider.radius);
        comp = GameObject.FindWithTag("vcam1");
        TornadoAudioManager = AudioObject.GetComponent<AudioManager>(); //create an audiomanager object to control only the tornado part audio.
        TornadoAudioManager.playSmallTornadoSource(); //since the tornado starting from small, so playing small at the beginning.
        TornadoAudioManager.StartPlayBgm();
        TornadoAudioManager.StopMenuBgm();

        currentCam = comp.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
        speedLine.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            comp.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = currentCam * 1.3f;
            speedLine.SetActive(true);
        }
        else
        {
            comp.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = currentCam;
            speedLine.SetActive(false);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        //wanghuai wrote this part, so ask him.
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
    public int returnTagLevel(Collider other)
    {
        //this part is comparing tag making these tags into cases for future purpose. and easier to adjust.
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
        // object moving derection
        Vector3 direction = transform.position - other.transform.position;
        float distance = Vector3.Distance(transform.position, other.transform.position);
        // Check if the other collider is within the suction range of the tornado
        if (distance <= radius)
        {
            // Calculate the strength of the suction force based on the distance between the tornado and the other collider
            float magnitude = force * (1 - distance / radius);

            // Add upward force to the suction direction
            Vector3 upVector = Vector3.up * 1f;
            Vector3 suctionDirection = (direction.normalized + upVector).normalized;

            // Apply the suction force to the other collider in the direction of the tornado
            other.transform.position += suctionDirection * magnitude * Time.deltaTime;

            // If the other collider is close enough to the tornado, destroy it
            if (distance <= destroyArea)
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
        Vector3 moveUp = new Vector3(0f,levelUpScaleIncrease, 0f);
        tornadoLevel++; //adding tornadolevels integer
        changeForce(cForce() + tornadoLevel * levelUpForce); //change force bigger.
        changeRadius(cRadius() + tornadoLevel * levelUpRadius); //change radius bigger
        transform.position += moveUp;
        this.gameObject.transform.localScale += new Vector3(levelUpScaleIncrease, levelUpScaleIncrease, levelUpScaleIncrease); //make the scale bigger
        destroyArea += levelUpScaleIncrease;
        
        //this part could be better but I am sleepy now. basically get the level and the changing sound requirement, if true change to next level sound.
        if(tornadoLevel == middleLevelAudio)
        {
            TornadoAudioManager.levelUpSoundEffect();
        }
        if(tornadoLevel == largeLevelAudio)
        {
            TornadoAudioManager.levelUpSoundEffect();
        }
        comp.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance += radius / CameraDisScale / 0.5f;
        currentCam = comp.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
        //above is a camera things, to make every level up zoom bigger just change the number in the function smaller, vice versa.
        //Debug.Log(force+ " " + radius);

        OnLevelUp?.Invoke(tornadoLevel);
    }
    public float cForce()
    {
        return force; //return current force
    }
    public void changeForce(float nForce)
    {
        force = nForce; //changing to new force
    }
    public float cRadius()
    {
        return radius; //return current radius
    }
    public void changeRadius(float nRadius)
    {
        radius = nRadius; //changing to new radius
    }
}  