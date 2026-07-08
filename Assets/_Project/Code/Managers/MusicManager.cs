using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioClip backgroundMusic;
    
    AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(audioClip);
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }       
}
