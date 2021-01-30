using UnityEngine;

namespace GGJ.Gameplay
{
    public class Chest : MonoBehaviour
    {
        public void Collected()
        {
            Destroy(gameObject);
        }
    }
}