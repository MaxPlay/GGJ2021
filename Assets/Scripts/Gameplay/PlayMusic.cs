using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    private static PlayMusic instance;
    public static PlayMusic Instance { get => instance; }

    [SerializeField]
    AudioSource audio;

    internal float GetStrength()
    {
        return audio.volume;
    }

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicStrength(float value)
    {
        audio.volume = value;
    }
}
