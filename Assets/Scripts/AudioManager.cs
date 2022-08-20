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

    public AudioSource musicPlayer;
    public AudioSource effectPlayer;
    public AudioClip[] background_audioClipArray;
    public AudioClip hit;
    public AudioClip death;
    public AudioClip shot;
    public AudioClip grow;
    public AudioClip shrink;
    public AudioClip lightning;
    public AudioClip multiBall;


    private void Start()
    {
        musicPlayer.clip = RandomClip();
        musicPlayer.Play();
    }

    private void Update()
    {
        //if (BricksManager.Instance.CurrentLevel == 5)
        //{
        //    musicPlayer.Pause();
        //    musicPlayer.clip = background_audioClipArray[4];
        //    //musicPlayer.volume = 0.07f;
        //    musicPlayer.Play();
        //}
        if (musicPlayer.isPlaying == false)
        {
            musicPlayer.Play();
        }
    }

    AudioClip RandomClip()
    {
        return background_audioClipArray[Random.Range(0, background_audioClipArray.Length-1)];
    }
}
