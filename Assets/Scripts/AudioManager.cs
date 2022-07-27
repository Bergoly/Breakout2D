using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    static AudioManager _instance;

    public static AudioManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public AudioSource audioSource;
    public AudioClip[] background_audioClipArray;
    public AudioClip hit;
    public AudioClip death;
    

    private void Start()
    {
        audioSource.PlayOneShot(RandomClip());
    }

    AudioClip RandomClip()
    {
        return background_audioClipArray[Random.Range(0, background_audioClipArray.Length)];
    }
}
