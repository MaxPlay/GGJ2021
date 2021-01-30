using UnityEngine;

public class Cloud : MonoBehaviour
{
    float Speed;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
    }

    public void SetCloudSpeed(float speed)
    {
        Speed = speed;
    }
}
