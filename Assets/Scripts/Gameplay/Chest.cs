using UnityEngine;

namespace GGJ.Gameplay
{
    public class Chest : MonoBehaviour
    {
        public void Collected()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            Animator animator = GetComponentInParent<Animator>();
            Debug.Assert(animator != null, "Animator is missing in Chest");
            animator.SetFloat("Offset", Random.value);
        }
    }
}