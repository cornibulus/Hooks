using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool IsOn { get; set; }

    public AudioSource winAudio;
    public AudioSource failAudio;

    private void OnLevelWasLoaded()
    {

    }

    public void PlayWinAudio()
    {
        //interrupt level music
        PlayAudioIfOn(winAudio);
    }

    public void PlayFailAudio()
    {
        //interrupt level music
        PlayAudioIfOn(failAudio);
    }

    private void PlayAudioIfOn(AudioSource audioSource)
    {
        if (this.IsOn && audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
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
