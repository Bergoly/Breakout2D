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
    public AudioClip zelda;
    public AudioClip hit;
    public AudioClip death;
    public AudioClip shot;
    public AudioClip grow;
    public AudioClip shrink;
    public AudioClip lightning;
    public AudioClip multiBall;

    private void Start()
    {
        audioSource.PlayOneShot(RandomClip(), 0.4f);
    }

    private void Update()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(RandomClip(), 0.4f);
        }
        //if (BricksManager.Instance.CurrentLevel == 5){
        //    audioSource.Stop();
        //    audioSource.PlayOneShot(zelda, 0.4f);
        //}
    }

    AudioClip RandomClip()
    {
        return background_audioClipArray[Random.Range(0, background_audioClipArray.Length)];
    }
}
