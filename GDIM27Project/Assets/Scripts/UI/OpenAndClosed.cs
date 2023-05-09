using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenAndClosed : MonoBehaviour
{
    public GameObject setting; // settingUI
    public Button openButton; // OpenButton
    public Button closeButton; // closeButton

    private void Start()
    {
        //Click Button to open and close UI
        openButton.onClick.AddListener(OpenSetting);
        closeButton.onClick.AddListener(CloseSetting);
        setting.SetActive(false);
    }

    private void OpenSetting()
    {
        // Open the setting UI
        setting.SetActive(true);

        // Pause game time
        Time.timeScale = 0;
    }

    private void CloseSetting()
    {
        // Close the setting UI
        setting.SetActive(false);

        // Resume game time
        Time.timeScale = 0;
    }
}
