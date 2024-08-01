using UnityEngine;
using UnityEngine.Serialization;

namespace Players
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _minZ;
        [SerializeField] private float _maxZ;

        private void Update()
        {
            float moveHorizontal = _joystick.Horizontal;
            float moveVertical = _joystick.Vertical;

            MoveCharacter(moveHorizontal, moveVertical);
        }

        private void MoveCharacter(float moveHorizontal, float moveVertical)
        {
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * (_moveSpeed * Time.deltaTime);
            Vector3 newPosition = transform.position + movement;

            newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, _minZ, _maxZ);

            transform.position = newPosition;
        }

        
        public void AddMoveSpeed(int percentage)
        {
            if (percentage is < -100 or > 100)
            {
                Debug.LogWarning("Percentage must be between -100 and 100.");
                return;
            }

            float newMoveSpeed = _moveSpeed * (1 + (percentage / 100f));

            _moveSpeed = Mathf.Max(0.01f, newMoveSpeed);
        }
    }
}