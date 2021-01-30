using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.UI
{
    public class UIController : MonoBehaviour
    {

        public static UIController Instance;

        List<IUIView> ViewStack = new List<IUIView>();

        // Falls wir UI Audio haben wieder aktivieren
        //public static AudioSource Audio { get { return instance.GetComponent<AudioSource>(); } }
        //public AudioClip audioClipClick;

        void Awake()
        {
            if (Instance == null) { Instance = this; }
        }

        void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach (IUIView uiView in ViewStack)
            {
                uiView.UIUpdate(deltaTime);
            }
        }

        public static void ChangeViewState(IUIView view, bool setActive)
        {
            Instance.StartCoroutine(Instance.ChangeUIState(view, setActive));
        }

        IEnumerator ChangeUIState(IUIView view, bool setActive)
        {
            yield return new WaitForEndOfFrame();
            if (setActive)
            {
                ViewStack.Insert(0, view);
            }
            else
            {
                ViewStack.Remove(view);
            }
            view.SetActive(setActive);
        }

        public static int StackPosition(IUIView view)
        {
            if (!Instance.ViewStack.Contains(view)) { return -1; }
            return Instance.ViewStack.IndexOf(view);
        }
    }
}
