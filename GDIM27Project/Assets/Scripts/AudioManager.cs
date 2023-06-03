using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [Header("BGM")]//存放背景音乐
    public AudioClip playBgm;
    public AudioClip menuBgm;


    [Header("龙卷风音效")]//存放龙卷风音效
    public AudioClip smallTornado;
    public AudioClip middleTornado;
    public AudioClip strongTornado;
    public AudioClip Collision;
    public AudioClip Suction;

    [Header("Audio Settings")]
    public float bgmVolume = 1.0f;
    public float tornadoVolume = 1.0f;
    public Slider bgmVolumeSlider; // 需要在 Unity 的 Inspector 中设置
    public Slider tornadoVolumeSlider; // 需要在 Unity 的 Inspector 中设置

    [Header("UI音效")] //存放UI音效
    public AudioClip jiFenZengJia; //积分增加
    public AudioClip jiFenDaoDa; //积分到达
    public AudioClip countDown;
    public AudioClip UIclick;

    AudioSource smallTornadoSource;
    AudioSource middleTornadoSource;
    AudioSource strongTornadoSource;
    AudioSource playBgmSource;
    AudioSource menuBgmSource;
    AudioSource jiFenZengJiaSource;
    AudioSource jiFenDaoDaSource;
    AudioSource UIclickSource;
    AudioSource collisionSource;
    AudioSource suctionSource;

    private int count = 0;

    private void Start()
    {


        smallTornadoSource = gameObject.AddComponent<AudioSource>();
        middleTornadoSource = gameObject.AddComponent<AudioSource>();
        strongTornadoSource = gameObject.AddComponent<AudioSource>();
        playBgmSource = gameObject.AddComponent<AudioSource>();
        menuBgmSource = gameObject.AddComponent<AudioSource>();
        UIclickSource = gameObject.AddComponent<AudioSource>();
        collisionSource = gameObject.AddComponent<AudioSource>();
        suctionSource = gameObject.AddComponent<AudioSource>();
        jiFenZengJiaSource = gameObject.AddComponent<AudioSource>();
        StartMenuBgm();

        float storedBgmVolume = PlayerPrefs.GetFloat("BgmVolume", 1.0f);

        float storedTornadoVolume = PlayerPrefs.GetFloat("TornadoVolume", 1.0f);
        // 设置背景音乐音量滑动条初始值
        bgmVolumeSlider.value = storedBgmVolume;

        // 设置龙卷风音效音量滑动条初始值
        tornadoVolumeSlider.value = storedTornadoVolume;


        // 设置背景音乐音源的音量
        playBgmSource.volume = storedBgmVolume;

        // 设置龙卷风音效音源的音量
        smallTornadoSource.volume = storedTornadoVolume;
        middleTornadoSource.volume = storedTornadoVolume;
        strongTornadoSource.volume = storedTornadoVolume;

        bgmVolumeSlider.onValueChanged.AddListener(ChangeBGMVolume);
        tornadoVolumeSlider.onValueChanged.AddListener(ChangeTornadoVolume);
    }


    public void StartMenuBgm()//播放主菜单BGM
    {
        menuBgmSource.clip = menuBgm;
        menuBgmSource.loop = true;
        menuBgmSource.Play();
    }

    public void StopMenuBgm()//停止主菜单BGM
    {
        menuBgmSource.Stop();
    }

    public void StartPlayBgm()//播放游玩BGM
    {
        playBgmSource.clip = playBgm;
        playBgmSource.loop = true;
        playBgmSource.Play();
    }

    public void levelUpSoundEffect()//龙卷风音效改变
    {
        if (count == 0)
        {
            stopSmallTornadoSource();
            playMediumTornadoSource();
        }
        if (count == 1)
        {
            stopMediumTornadoSource();
            playLargeTornadoSource();
        }
        count++;
    }

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
        playBgmSource.volume = newVolume;
        PlayerPrefs.SetFloat("BgmVolume", newVolume);
    }

    public void ChangeTornadoVolume(float newVolume)
    {
        tornadoVolume = newVolume;
        smallTornadoSource.volume = newVolume;
        middleTornadoSource.volume = newVolume;
        strongTornadoSource.volume = newVolume;
        PlayerPrefs.SetFloat("TornadoVolume", newVolume);
    }

    public void playUIclickSource()//播放UI音效
    {
        UIclickSource.clip = UIclick;
        UIclickSource.PlayOneShot(UIclick);
    }

    public void playCollisionSource() //播放碰撞（不可吸入）音效
    {
        collisionSource.clip = Collision;
        collisionSource.PlayOneShot(Collision);
    }

    public void playSuctionSource() //播放吸入音效
    {
        suctionSource.clip = Suction;
        suctionSource.PlayOneShot(Suction);
    }

    public void playJiFenZengJiaSource()
    {
        jiFenZengJiaSource.clip = jiFenZengJia;
        jiFenZengJiaSource.PlayOneShot(jiFenZengJia);
    }
}
