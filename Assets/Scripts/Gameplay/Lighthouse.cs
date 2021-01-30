using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    public class Ship : MonoBehaviour
    {
        bool IsActive;
        [SerializeField] Light directionalLight;
        [SerializeField] GameObject lightPlane;
        [SerializeField] GameObject lightPivot;
        void Update()
        {
            HandleInputs();
            if (IsActive) { AlignLight(); }
        }

        void HandleInputs()
        {
            if (IsActive && Input.GetKeyDown(KeyCode.F))
            {
                IsActive = false;
            }else if (!IsActive && Input.GetKeyDown(KeyCode.F))
            {
                //REMOVE LATER
                IsActive = true;
            }
        }

        void AlignLight()
        {
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            lightPivot.transform.rotation = Quaternion.AngleAxis (-angle, Vector3.up);
        }

        //REMOVE LATER
        void Start()
        {
            IsActive = true;
        }
    }
}
