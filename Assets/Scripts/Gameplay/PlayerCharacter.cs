using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    GameObject visuals;

    [SerializeField]
    Transform[] paths;

    [SerializeField, Range(0, 1)]
    private float currentPosition;

    [SerializeField]
    private float speed;

    [SerializeField]
    UnityEvent hitRightBorder = new UnityEvent();

    [SerializeField]
    UnityEvent hitLeftBorder = new UnityEvent();

    [SerializeField]
    UnityEvent leaveBorder = new UnityEvent();


    public float CurrentPosition
    {
        get
        {
            return currentPosition;
        }
        set
        {
            float prevPosition = currentPosition;
            currentPosition = Mathf.Clamp01(value);
            if (prevPosition != currentPosition)
                UpdatePosition();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 0.5f;
        transform.position = GetCurrentPosition();
    }

    private void Update()
    {
        //TODO: Replace this with actual Input-System
        if (Input.GetKey(KeyCode.D))
            MoveRight();
        if (Input.GetKey(KeyCode.A))
            MoveLeft();
    }

    public void SetPosition(float position)
    {
        CurrentPosition = position;
    }

    public void MoveLeft()
    {
        CurrentPosition -= (speed * Time.deltaTime);
    }

    public void MoveRight()
    {
        CurrentPosition += (speed * Time.deltaTime);
    }

    private void UpdatePosition()
    {
        transform.position = GetCurrentPosition();
        if (CurrentPosition == 0)
        {
            hitLeftBorder.Invoke();
            visuals.SetActive(false);
        }
        else if (CurrentPosition == 1)
        {
            hitRightBorder.Invoke();
            visuals.SetActive(false);
        }
        else if (!visuals.activeSelf)
        {
            leaveBorder.Invoke();
            visuals.SetActive(true);
        }
    }

    private Vector3 GetCurrentPosition()
    {
        if (paths.Length < 2)
            return Vector3.zero;
        if (currentPosition == 1)
            return paths[paths.Length - 1].position;
        float lineCount = paths.Length - 1 - 0f;
        int lineIndex = (int)(currentPosition * lineCount);
        float positionOnLine = (currentPosition % (1.0f / lineCount)) * lineCount;
        return paths[lineIndex].position + Vector3.LerpUnclamped(paths[lineIndex + 1].position - paths[lineIndex].position, Vector3.zero, 1 - positionOnLine);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < paths.Length - 1; i++)
        {
            Gizmos.DrawLine(paths[i].position, paths[i + 1].position);
        }
        Gizmos.DrawSphere(GetCurrentPosition(), 0.1f);
    }
}
