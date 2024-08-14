using UnityEngine;

public class EnemyBodyPartLookVisual : MonoBehaviour
{
    [SerializeField] private Transform _lookAtTransform;
        
    private void Update()
    {
        if (!_lookAtTransform) return;

        // Get the direction to the target
        Vector3 direction = _lookAtTransform.position - transform.position;

        // Keep only the x and y components of the direction fixed
        direction.x = 0;  // Neutralize x to keep rotation fixed on x-axis
        direction.y = 0;  // Neutralize y to keep rotation fixed on y-axis

        // Update the rotation to look at the target, but only changing the z rotation
        transform.rotation = Quaternion.LookRotation(direction);

        // Adjust the local x and y angles to stay fixed at 90 degrees
        Vector3 localEulerAngles = transform.localEulerAngles;
        localEulerAngles.x = 90f;
        localEulerAngles.y = 90f;
        transform.localEulerAngles = localEulerAngles;
    }



}