using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] protected Transform trackingTarget;

    void Update()
    {
        transform.position = new Vector3(trackingTarget.position.x,
             trackingTarget.position.y, transform.position.z);
    }
}