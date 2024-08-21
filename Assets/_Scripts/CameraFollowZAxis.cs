using UnityEngine;

public class CameraFollowZAxis : MonoBehaviour
{
    public Transform target;

    private float initialZDistance;

    private void Start()
    {
        initialZDistance = transform.position.z - target.position.z;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 newPosition = transform.position;
        newPosition.z = target.position.z + initialZDistance;

        transform.position = newPosition;
    }
}