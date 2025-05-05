using UnityEngine;

public class CloudLooper : MonoBehaviour
{
    public Transform[] clouds;
    public float speed = 2f;
    private float width;

    void Start() => width = clouds[0].GetComponent<SpriteRenderer>().bounds.size.x;

    void Update()
    {
        foreach (var cloud in clouds)
        {
            cloud.position += Vector3.right * speed * Time.deltaTime;
            if (cloud.position.x > width * 1.5f)
            {
                float leftMostX = Mathf.Min(clouds[0].position.x, clouds[1].position.x, clouds[2].position.x);
                cloud.position = new Vector3(leftMostX - width, cloud.position.y, cloud.position.z);
            }
        }
    }
}