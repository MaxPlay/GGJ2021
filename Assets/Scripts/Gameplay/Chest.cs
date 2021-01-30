using UnityEngine;

namespace GGJ.Gameplay
{
    public class Chest : MonoBehaviour
    {
        public void Collected()
        {
            // TODO: We could delete the chest here or just disable it. I go with disable, but this is something we need to discuss
            gameObject.SetActive(false);
        }
    }
}