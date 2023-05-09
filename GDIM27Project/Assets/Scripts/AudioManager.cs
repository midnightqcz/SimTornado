using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    [Header("BGM")]//存放背景音乐
    public AudioClip bgm; 


    [Header("龙卷风音效")]//存放龙卷风音效
    public AudioClip smallTornado;
    public AudioClip middleTornado;
    public AudioClip strongTornado;

    AudioSource smallTornadoSource;
    AudioSource middleTornadoSource;
    AudioSource strongTornadoSource;
    AudioSource bgmSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        smallTornadoSource = gameObject.AddComponent<AudioSource>();
        middleTornadoSource = gameObject.AddComponent<AudioSource>();
        strongTornadoSource = gameObject.AddComponent<AudioSource>();
        bgmSource= gameObject.AddComponent<AudioSource>();
        StartLevelAudio();
    }

    public void StartLevelAudio()
    {
        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();
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

    //int windLevel = 1;
    //smallTornadoSource.clip = smallTornado;
    // smallTornadoSource.loop = true;
    //smallTornadoSource.Play();


}
