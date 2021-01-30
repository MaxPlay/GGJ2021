using UnityEngine;

namespace GGJ.Gameplay
{
    public class Harbor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ship"))
            {
                Ship ship = other.GetComponent<Ship>();
                Debug.Assert(ship != null, "");
                GameManager.Instance.ShipReachedHarbor(ship);
                ship.ReachedHarbor();
            }
        }
    }
}
