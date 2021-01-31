using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOption : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    private void Start()
    {
        if(PlayMusic.Instance)
            slider.value = PlayMusic.Instance.GetStrength();
    }

    public void SetStrength(float value)
    {
        if(PlayMusic.Instance)
            PlayMusic.Instance.SetMusicStrength(value);
    }
}
