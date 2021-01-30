using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Gameplay
{
    public class WindManager : MonoBehaviour
    {
        [SerializeField] Vector2 windDirection;
        [Range(0f, 1f)] [SerializeField] float minWindStrength;
        [Range(0f, 1f)] [SerializeField] float maxWindStrength;
        [SerializeField] GameObject[] cloudPrefabs;
        [SerializeField] Vector3 cloudSpawnVector;
        [SerializeField] float cloudDistanceUntilDeath;
        [Range(0f, 1f)] [SerializeField] float cloudAmount;

        public Vector2 GetWindSpeed()
        {
            return windDirection * Random.Range(minWindStrength, maxWindStrength);
        }
    }
}
