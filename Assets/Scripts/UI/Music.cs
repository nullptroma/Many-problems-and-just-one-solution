using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static bool Enable
    {
        get => _enable;
        set
        {
            _enable = value;
            _instance.Save();
            if(_enable)
                _instance.Play();
            else
                _instance.Stop();
        }
    }
    private static bool _enable;
    private static Music _instance;
    [SerializeField] private AudioClip[] musicAudioClips;
    [SerializeField] private AudioSource source;
    
    void Awake()
    {
        Load();
        _instance = this;
        if (Enable)
            Play();
    }

    void Load()
    {
        _enable = PlayerPrefs.GetInt("Music", 0) != 0;
    }

    void Save()
    {
        PlayerPrefs.SetInt("Music",_enable?1:0);
        PlayerPrefs.Save();
    }
    
    private void Play()
    {
        StartCoroutine(PlayBackgroudMusic());
    }
    
    private void Stop()
    {
        source.Stop();
    }

    IEnumerator PlayBackgroudMusic()
    {
        int musicIndex = 0;
        while (musicAudioClips.Length > 0)
        {
            float waitTime = musicAudioClips[musicIndex].length+2f;
            source.PlayOneShot(musicAudioClips[musicIndex]);

            musicIndex++;
            if (musicIndex >= musicAudioClips.Length)
            {
                musicIndex = 0;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }
}
