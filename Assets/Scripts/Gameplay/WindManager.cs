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
        [SerializeField] Vector3 cloudLayerSize;
        [Range(1, 20)] [SerializeField] int cloudAmount;

        public Vector2 GetWindSpeed()
        {
            return windDirection * Random.Range(minWindStrength, maxWindStrength);
        }

        void Start()
        {
            SpawnStaticClouds();
        }

        void SpawnStaticClouds()
        {
            for (int i = 0; i < cloudAmount; i++)
            {
                float x = Random.Range(cloudSpawnVector.x, cloudLayerSize.x);
                float y = Random.Range(cloudSpawnVector.y, cloudLayerSize.y);
                float z = Random.Range(cloudSpawnVector.z, cloudLayerSize.z);
                Vector3 spawn = new Vector3(x, y, z);

                int cloudIndex = Random.Range(0, cloudPrefabs.Length);

                Instantiate(cloudPrefabs[cloudIndex], spawn, Quaternion.identity);

                //if (windDirection == Vector2.zero)
                //{
                //}
            }
        }
    }
}
