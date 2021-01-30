using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace GGJ.Gameplay
{
    public class Lighthouse : MonoBehaviour
    {
        public enum states { active, passive, disabled }
        states LighthouseState;
        [SerializeField] Light directionalLight;
        [SerializeField] GameObject lightPlane;
        [SerializeField] GameObject lightPivot;
        [SerializeField] float rotationSpeed = 60f;
        [SerializeField] Animator switchAnimator;
        [SerializeField] UnityEvent onLightTurnedOn = new UnityEvent();
        [SerializeField] UnityEvent onLightTurnedOff = new UnityEvent();
        void Update()
        {
            HandleInputs();
        }

        void HandleInputs()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                lightPivot.SetActive(!lightPivot.activeSelf);
                if (lightPivot.activeSelf)
                {
                    LighthouseState = states.active;
                    switchAnimator.SetBool("Active", true);
                    onLightTurnedOn.Invoke();
                }
                if (!lightPivot.activeSelf)
                {
                    LighthouseState = states.disabled;
                    switchAnimator.SetBool("Active", false);
                    onLightTurnedOff.Invoke();
                }
            }

            if (LighthouseState == states.active)
            {
                if (lightPivot.activeSelf && Input.GetKey(KeyCode.E))
                {
                    lightPivot.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                }
                else if (lightPivot.activeSelf && Input.GetKey(KeyCode.Q))
                {
                    lightPivot.transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed);
                }
            }
        }

        void OnEnable()
        {
            LighthouseState = states.active;
            lightPivot.SetActive(true);
            switchAnimator.SetBool("Active", true);
            onLightTurnedOn.Invoke();
        }
    }
}
