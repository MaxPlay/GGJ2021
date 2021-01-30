using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public static Wind Instance;
    [SerializeField] Vector2 windDirection;
    [Range(0f, 1f)] [SerializeField] float minWindStrength;
    [Range(0f, 1f)] [SerializeField] float maxWindStrength;


    void Awake()
    {
        Instance = this;
    }

    public Vector2 GetWindSpeed()
    {
        return windDirection * Random.Range(minWindStrength, maxWindStrength);
    }
}
