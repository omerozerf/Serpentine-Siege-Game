using System;
using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPartCollision : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart _enemyBodyPart;
        [SerializeField] private LayerMask _collisionMask;

        public event Action<int> OnShooted;

        private float m_CheckTime;
        private bool isReversing = false;

        // Cooldown-related fields
        private float _cooldownTime = 0.5f; // Cooldown time in seconds
        private float _cooldownTimer = 0f; // Timer to track cooldown
        private bool _wasPathBlocked = false; // Tracks the last state

        public EnemyBodyPart GetEnemyBodyPart()
        {
            return _enemyBodyPart;
        }

        public void OnShoot(int damage)
        {
            OnShooted?.Invoke(damage);
        }

        public bool IsPathBlocked()
        {
            RaycastHit hit;

            // SphereCast to check for collision
            if (Physics.SphereCast(transform.position, 1.5f, -transform.forward, out hit, 1.5f, _collisionMask))
            {
                // If the hit object is not this object, path is blocked
                if (hit.collider.gameObject != gameObject)
                {
                    _wasPathBlocked = true;
                    _cooldownTimer = 0f; // Reset cooldown timer
                    return true;
                }
            }

            // If path was blocked, start cooldown timer
            if (_wasPathBlocked)
            {
                _cooldownTimer += Time.deltaTime;

                // If cooldown timer exceeds the cooldown time, reset
                if (_cooldownTimer >= _cooldownTime)
                {
                    _wasPathBlocked = false;
                }

                return false;
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            // Set gizmo color to black to represent the ray path
            Gizmos.color = Color.black;

            // Raycast parameters
            Vector3 origin = transform.position;
            Vector3 direction = -transform.forward;
            float rayLength = 1.5f;
            float sphereRadius = 1.5f;

            // Draw the ray path
            Gizmos.DrawRay(origin, direction * rayLength);

            // Set gizmo color to red to represent the sphere at the end of the ray
            Gizmos.color = Color.red;

            // Draw the sphere at the end of the ray path to represent the SphereCast's detection area
            Gizmos.DrawWireSphere(origin + direction * rayLength, sphereRadius);
        }
    }
}
