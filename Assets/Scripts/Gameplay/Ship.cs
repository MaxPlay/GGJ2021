using UnityEngine;

namespace GGJ.Gameplay
{
    public class Ship : MonoBehaviour
    {
        public bool CanBeControlledByLighthouse { get; private set; }

        public bool HasChest { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Chest"))
            {
                Chest chest = other.GetComponent<Chest>();
                Debug.Assert(chest != null, $@"GameObject with Tag ""Chest"" has no component of type ""{nameof(Chest)}""");
                chest.Collected();
                HasChest = true;
            }
        }

        public void ReachedHarbor()
        {
            CanBeControlledByLighthouse = false;
            // We could do some animation here, or we can just remove the ship. At this point, I'll just do the latter
            gameObject.SetActive(false);
        }
    }
}