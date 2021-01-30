using UnityEngine;

public class Cloud : MonoBehaviour
{
    float Speed;
    Rect CloudArea;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
        if (transform.position.x < CloudArea.x)
        {
            transform.position = new Vector3(CloudArea.x + CloudArea.width, transform.position.y, transform.position.z);
        }else if (transform.position.x > CloudArea.x + CloudArea.width)
        {
            transform.position = new Vector3(CloudArea.x, transform.position.y, transform.position.z);
        }
    }

    public void SetCloudSpeed(float speed)
    {
        this.Speed = speed;
    }

    public void SetCloudArea(Rect cloudArea)
    {
        this.CloudArea = cloudArea;
    }
}
