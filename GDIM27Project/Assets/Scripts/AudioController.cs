using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    /*public Slider volumeSlider; // Reference to the volume slider
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
    */

    public Slider bgmVolumeSlider; // Reference to the background music volume slider
    public Slider tornadoVolumeSlider; // Reference to the tornado sound volume slider
    public AudioSource bgmAudioSource; // Reference to the background music audio source
    public AudioSource tornadoAudioSource; // Reference to the tornado sound audio source

    void Start()
    {
        // 获取存储的背景音乐音量设置
        float storedBgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1.0f);

        // 获取存储的龙卷风音效音量设置
        float storedTornadoVolume = PlayerPrefs.GetFloat("TornadoVolume", 1.0f);

        // 设置背景音乐音量滑动条初始值
        bgmVolumeSlider.value = storedBgmVolume;

        // 设置龙卷风音效音量滑动条初始值
        tornadoVolumeSlider.value = storedTornadoVolume;

        // 设置背景音乐音源的音量
        bgmAudioSource.volume = storedBgmVolume;

        // 设置龙卷风音效音源的音量
        tornadoAudioSource.volume = storedTornadoVolume;

        // 监听背景音乐音量滑动条值改变事件
        bgmVolumeSlider.onValueChanged.AddListener(delegate { OnBgmVolumeChanged(); });

        // 监听龙卷风音效音量滑动条值改变事件
        tornadoVolumeSlider.onValueChanged.AddListener(delegate { OnTornadoVolumeChanged(); });
    }

    void OnBgmVolumeChanged()
    {
        // 更新背景音乐音源的音量
        float newVolume = bgmVolumeSlider.value;
        bgmAudioSource.volume = newVolume;

        // 存储背景音乐音量设置
        PlayerPrefs.SetFloat("BgmVolume", newVolume);
    }

    void OnTornadoVolumeChanged()
    {
        // 更新龙卷风音效音源的音量
        float newVolume = tornadoVolumeSlider.value;
        tornadoAudioSource.volume = newVolume;

        // 存储龙卷风音效音量设置
        PlayerPrefs.SetFloat("TornadoVolume", newVolume);
    }

}