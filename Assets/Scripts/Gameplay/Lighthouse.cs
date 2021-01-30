using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GGJ.Gameplay
{
    public class Lighthouse : MonoBehaviour
    {
        public enum states { active, passive, diabled }
        states LighthouseState;
        [SerializeField] Light directionalLight;
        [SerializeField] GameObject lightPlane;
        [SerializeField] GameObject lightPivot;
        [SerializeField] float rotationSpeed = 60f;
        void Update()
        {
            HandleInputs();
        }

        void HandleInputs()
        {
            if (LighthouseState == states.active && Input.GetKeyDown(KeyCode.F))
            {
                LighthouseState = states.passive;
            }
            else if (LighthouseState != states.active && Input.GetKeyDown(KeyCode.F))
            {
                //REMOVE LATER
                LighthouseState = states.active;
            }

            if (LighthouseState == states.active)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    lightPivot.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    lightPivot.transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed);
                }
            }
        }
        //REMOVE LATER
        void Start()
        {
            LighthouseState = states.active;
        }
    }
}
