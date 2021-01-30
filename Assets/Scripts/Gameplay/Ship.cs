using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GGJ.Gameplay
{
    public class Ship : MonoBehaviour
    {
        public bool CanBeControlledByLighthouse { get; private set; }

        public bool HasChest { get; private set; }
        [SerializeField] float speed;

        List<Transform> InfuencingLightBeams = new List<Transform>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Chest"))
            {
                Chest chest = other.GetComponent<Chest>();
                Debug.Assert(chest != null, $@"GameObject with Tag ""Chest"" has no component of type ""{nameof(Chest)}""");
                chest.Collected();
                HasChest = true;
            }
            else if (other.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
            else if (other.CompareTag("Lightbeam"))
            {
                if (!InfuencingLightBeams.Contains(other.transform.parent))
                {
                    InfuencingLightBeams.Add(other.transform.parent);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Lightbeam"))
            {
                if (InfuencingLightBeams.Contains(other.transform.parent))
                {
                    InfuencingLightBeams.Remove(other.transform.parent);
                }
            }
        }

        public void ReachedHarbor()
        {
            CanBeControlledByLighthouse = false;
            // We could do some animation here, or we can just remove the ship. At this point, I'll just do the latter
            gameObject.SetActive(false);
        }

        void Update()
        {
            //ADD WIND LATER
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (InfuencingLightBeams.Count > 0) { Debug.Log("I see a light!"); }
        }
    }
}