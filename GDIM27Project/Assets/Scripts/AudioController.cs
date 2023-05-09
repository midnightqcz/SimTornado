using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the volume slider
    public AudioSource audioSource; // Reference to the audio source

    void Start()
    {
        // Initialize the volume to the slider's initial value
        audioSource.volume = volumeSlider.value;
        // Listen to the value changed event of the volume slider
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
    }

    void OnVolumeChanged()
    {
        // Update the audio source's volume based on the value of the volume slider
        audioSource.volume = volumeSlider.value;
    }
}
