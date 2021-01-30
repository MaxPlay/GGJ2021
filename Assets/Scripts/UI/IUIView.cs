using UnityEngine;

namespace GGJ.UI
{
    public abstract class IUIView : MonoBehaviour
    {
        public abstract void SetActive(bool active);
        public abstract void UIUpdate(float deltaTime);
    }
}