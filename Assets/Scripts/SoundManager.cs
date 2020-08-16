using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public bool IsOn { get; set; }

    public AudioSource hookAudio;

    public void PlayHookAudio()
    {
        PlayAudioIfOn(hookAudio);
    }

    private void PlayAudioIfOn(AudioSource audioSource)
    {
        if (this.IsOn && audioSource != null)
            audioSource.Play();
    }

    //Singleton boilerplate
    private static SoundManager _instance;

    public static SoundManager Instance
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
