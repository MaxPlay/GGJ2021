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
        float WindSpeed;
        [SerializeField] GameObject[] cloudPrefabs;
        [SerializeField] Vector3 cloudSpawnVector;
        [SerializeField] Vector3 cloudLayerSize;
        [Range(1, 20)] [SerializeField] int cloudAmount;

        public Vector2 GetWindSpeed()
        {
            return windDirection * WindSpeed;
        }

        void Start()
        {
            WindSpeed = Random.Range(minWindStrength, maxWindStrength);
            SpawnClouds();
        }

        void SpawnClouds()
        {
            for (int i = 0; i < cloudAmount; i++)
            {
                float x = Random.Range(cloudSpawnVector.x, cloudSpawnVector.x + cloudLayerSize.x);
                float y = Random.Range(cloudSpawnVector.y, cloudSpawnVector.y + cloudLayerSize.y);
                float z = Random.Range(cloudSpawnVector.z, cloudSpawnVector.z + cloudLayerSize.z);
                Vector3 spawn = new Vector3(x, y, z);

                int cloudIndex = Random.Range(0, cloudPrefabs.Length);

                GameObject cloud = Instantiate(cloudPrefabs[cloudIndex], spawn, Quaternion.identity);

                if (windDirection.x != 0f)
                {
                    Cloud cloudBehaviour = cloud.AddComponent<Cloud>();
                    cloudBehaviour.SetCloudArea(new Rect(cloudSpawnVector.x, cloudSpawnVector.y, cloudLayerSize.x, cloudLayerSize.y));
                    if (windDirection.x > 0f)
                    {
                        cloudBehaviour.SetCloudSpeed(-Random.Range(minWindStrength, maxWindStrength));
                    }
                    else { cloudBehaviour.SetCloudSpeed(Random.Range(minWindStrength, maxWindStrength)); }
                }
            }
        }
    }
}
