using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool IsOn { get; set; }

    public AudioSource winAudio;
    public AudioSource failAudio;

    private AudioSource levelAudio;

    private void OnLevelWasLoaded()
    {
        levelAudio = null;

        GameObject levelAudioObj = GameObject.FindWithTag("LevelAudio");
        if(levelAudioObj != null)
        {
            this.levelAudio = levelAudioObj.GetComponent<AudioSource>();
        }

        if(levelAudio != null)
        {
            levelAudio.mute = !this.IsOn;
            levelAudio.Play();
        }
    }

    public void PlayWinAudio()
    {
        if (levelAudio != null && levelAudio.isPlaying)
            levelAudio.Stop();

        PlayAudioIfOn(winAudio);
    }

    public void PlayFailAudio()
    {
        if (levelAudio != null && levelAudio.isPlaying)
            levelAudio.Stop();

        PlayAudioIfOn(failAudio);
    }

    private void PlayAudioIfOn(AudioSource audioSource)
    {
        if (this.IsOn && audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }

    private void Update()
    {
        if(levelAudio != null)
            levelAudio.mute = !this.IsOn;
    }

    //Singleton boilerplate
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
}
