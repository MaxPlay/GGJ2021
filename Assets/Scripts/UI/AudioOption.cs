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
        slider.value = PlayMusic.Instance.GetStrength();
    }

    public void SetStrength(float value)
    {
        PlayMusic.Instance.SetMusicStrength(value);
    }
}
