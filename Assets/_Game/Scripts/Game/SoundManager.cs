using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Models;
using UnityEngine;

public class SoundManager : SingletonPersistent<SoundManager>
{
    public static readonly string VOLUME_SFX_KEY_LOCAL = "sound";
    public static readonly string VOLUME_MUSIC_KEY_LOCAL = "music";
    [SerializeField] private AudioSource prefabAudioSource;
    [SerializeField] private int maxAudio = 4; 
    [SerializeField] private List<AudioClip> listAudio = new List<AudioClip>();
    private Queue<AudioSource> asQueue = new Queue<AudioSource>();
    private List<AudioSource> listASFX = new List<AudioSource>();
    private AudioSource asMusic;

    private bool _isSetVolumeFromLocal = false;
    public bool isSetVolumeFromLocal 
    {
        get
        {
            return this._isSetVolumeFromLocal;
        } 
        set
        {
            this._isSetVolumeFromLocal = value;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        this.InitAudioSource();
        this.asMusic = this.asQueue.Dequeue();
        this.asMusic.loop = true;
    }

    void Start()
    {
        this.PlayMusicBG();
        this.SetVolumeFromLocalStorage();
    }   

    public AudioSource PlaySound(ESoundID audioID, bool isLoop = false)
    {
        AudioSource a = this.GetAudioSource();
        if(a != null)
        {
            a.clip = this.listAudio[(int)audioID];
            a.Play();
            a.loop = isLoop;
            StartCoroutine(this.WaitForRepool(a));
        }
        return a;
    }

    IEnumerator WaitForRepool(AudioSource a)
    {
        yield return new WaitUntil(()=> a.isPlaying == false);
        this.Repool(a);
        // this.asQueue.Enqueue(a);
    }

    private void InitAudioSource()
    {
        for(int i = 0; i < maxAudio; i++)
        {
            AudioSource a = GameObject.Instantiate<AudioSource>(this.prefabAudioSource);
            this.asQueue.Enqueue(a);
            a.gameObject.transform.parent = this.gameObject.transform;
            if(i > 0)
            {
                this.listASFX.Add(a);
            }
        }
    }

    private AudioSource GetAudioSource()
    {
        if(this.asQueue.Count == 0) 
        {
            return null;
        }
        else
        {
            return this.asQueue.Dequeue();
        } 
    }

    public void Repool(AudioSource a)
    {
        if(a != null)
        {
            a.Stop();
            a.loop = false;
            this.asQueue.Enqueue(a);
        }
            
    }

    private void PlayMusicBG(int soundID = -1)
    {
        if(this.asMusic != null)
        {
            if(soundID == -1)
            {
                this.asMusic.clip = this.listAudio[(int)ESoundID.BGMusic];
                this.asMusic.Play();
            }
            else
            {
                this.asMusic.clip = this.listAudio[soundID];
                this.asMusic.Play();
            }  
        }
    }

    public void SetVolumeBGMusic(float volume)
    {
        this.asMusic.volume = Mathf.Clamp(volume, 0,1);
    }

    public void SetVolumeSFX(float volume)
    {
        foreach(var a in this.listASFX)
        {
            a.volume = Mathf.Clamp(volume, 0,1);
        }
        
    }

    public void SetVolume()
    {
        this.SetVolumeBGMusic(GameManager.UserData.sound_setting);
        this.SetVolumeSFX(GameManager.UserData.vfx_setting);
    }

    public void SetVolumeFromLocalStorage()
    {
        if(this.HasSavedLocal())
        {
            var volumeSFX = PlayerPrefs.GetFloat(SoundManager.VOLUME_SFX_KEY_LOCAL);
            var volumeMusicBG = PlayerPrefs.GetFloat(SoundManager.VOLUME_MUSIC_KEY_LOCAL);
            this.SetVolumeBGMusic(volumeMusicBG);
            this.SetVolumeSFX(volumeSFX);
            this.isSetVolumeFromLocal = true;
        }
    }

    public static void SaveVolume()
    {
        PlayerPrefs.SetFloat(SoundManager.VOLUME_MUSIC_KEY_LOCAL, GameManager.UserData.sound_setting);
        PlayerPrefs.SetFloat(SoundManager.VOLUME_SFX_KEY_LOCAL, GameManager.UserData.vfx_setting);
    }

    public bool HasSavedLocal()
    {
        return PlayerPrefs.HasKey(SoundManager.VOLUME_MUSIC_KEY_LOCAL) && PlayerPrefs.HasKey(SoundManager.VOLUME_SFX_KEY_LOCAL);
    }
}

public enum ESoundID
{
    BGMusic,
    ButtonBattle,
    Feed,
    ButtonClick,
    FusionComplete,
    ButtonSell,
    HatchEgg,
    Error,
    ExchangeComplete,
    FishInteractive,
    Notification,
    WaitEgg,
}
