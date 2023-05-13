using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
     public GameObject[] levelImages; // Set these in the Unity editor

    void OnEnable()
    {
        TornadoSuction.OnLevelUp += UpdateUI;
    }

    void OnDisable()
    {
        TornadoSuction.OnLevelUp -= UpdateUI;
    }

    void UpdateUI(int newLevel)
    {


        // Show the image for the new level
        if (newLevel > 0 && newLevel <= levelImages.Length)
        {
            levelImages[newLevel - 1].SetActive(true);
        }
    }
}
