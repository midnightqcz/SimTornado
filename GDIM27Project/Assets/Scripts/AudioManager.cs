using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 
public class AudioManager : MonoBehaviour
{
    [Header("BGM")]//存放背景音乐
    public AudioClip bgm; 


    [Header("龙卷风音效")]//存放龙卷风音效
    public AudioClip smallTornado;
    public AudioClip middleTornado;
    public AudioClip strongTornado;

    [Header("Audio Settings")]
    public float bgmVolume = 1.0f;
    public float tornadoVolume = 1.0f;
    public Slider bgmVolumeSlider; // 需要在 Unity 的 Inspector 中设置
    public Slider tornadoVolumeSlider; // 需要在 Unity 的 Inspector 中设置

    AudioSource smallTornadoSource;
    AudioSource middleTornadoSource;
    AudioSource strongTornadoSource;
    AudioSource bgmSource;

    private int count = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        smallTornadoSource = gameObject.AddComponent<AudioSource>();
        middleTornadoSource = gameObject.AddComponent<AudioSource>();
        strongTornadoSource = gameObject.AddComponent<AudioSource>();
        bgmSource= gameObject.AddComponent<AudioSource>();
        StartLevelAudio();

        bgmVolumeSlider.onValueChanged.AddListener(ChangeBGMVolume);
        tornadoVolumeSlider.onValueChanged.AddListener(ChangeTornadoVolume);
    }

    public void StartLevelAudio()
    {
        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();
    }
    
    public void levelUpSoundEffect()
    {
        if(count == 0)
        {
            stopSmallTornadoSource();
            playMediumTornadoSource();
        }
        if(count == 1)
        {
            stopMediumTornadoSource();
            playLargeTornadoSource();
        }
        count++;
    }

    //if (other.gameObject.tag == "Player")
    public void playSmallTornadoSource()
    {
        smallTornadoSource.clip = smallTornado;
        smallTornadoSource.loop = true;
        smallTornadoSource.Play();
    }
    public void stopSmallTornadoSource()
    {
        smallTornadoSource.Stop();
    }
    public void playMediumTornadoSource()
    {
        middleTornadoSource.clip = middleTornado;
        middleTornadoSource.loop = true;
        middleTornadoSource.Play();
    }
    public void stopMediumTornadoSource()
    {
        middleTornadoSource.Stop();
    }
    public void playLargeTornadoSource()
    {
        strongTornadoSource.clip = strongTornado;
        strongTornadoSource.loop = true;
        strongTornadoSource.Play();
    }
    public void stopLargeTornadoSource()
    {
        strongTornadoSource.Stop();
    }

    public void ChangeBGMVolume(float newVolume)
    {
        bgmVolume = newVolume;
        bgmSource.volume = newVolume;
    }

    public void ChangeTornadoVolume(float newVolume)
    {
        tornadoVolume = newVolume;
        smallTornadoSource.volume = newVolume;
        middleTornadoSource.volume = newVolume;
        strongTornadoSource.volume = newVolume;
    }



}
