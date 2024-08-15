using System;
using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPartCollision : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart _enemyBodyPart;
        [SerializeField] private LayerMask _collisionMask;
        [SerializeField] private GameObject _tailParticle;

        public event Action<int> OnShooted;

        private float m_CheckTime;
        private bool isReversing = false;

        // Cooldown-related fields
        private float _cooldownTime = 0.1f; // Cooldown time in seconds
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

        private void Update()
        {
            _tailParticle.SetActive(!IsPathBlocked());
        }

        public bool IsPathBlocked()
        {
            RaycastHit hit;
            
            // SphereCast to check for collision
            if (Physics.SphereCast(transform.position, 0.5f, -transform.forward, out hit, 1f, _collisionMask))
            {
                // If the hit object is not this object, path is blocked
                if (hit.collider.gameObject != gameObject)
                {
                    _wasPathBlocked = true;
                    _cooldownTimer = 0f; // Reset cooldown timer
                    return true;
                }
            }
            else
            {
                // No collision detected, increase cooldown timer
                _cooldownTimer += Time.deltaTime;

                // If no collision for 1 second, reset the blocked state
                if (_cooldownTimer >= _cooldownTime)
                {
                    _wasPathBlocked = false;
                }
            }

            if (!_wasPathBlocked)
            {
                Debug.Log(gameObject,gameObject);
            }
            
            // Return the current blocked state
            return _wasPathBlocked;
        }

        private void OnDrawGizmos()
        {
            // Set gizmo color to black to represent the ray path
            Gizmos.color = Color.black;

            // Raycast parameters
            Vector3 origin = transform.position;
            Vector3 direction = -transform.forward;
            float rayLength = 1f;
            float sphereRadius = 0.5f;

            // Draw the ray path
            Gizmos.DrawRay(origin, direction * rayLength);

            // Set gizmo color to red to represent the sphere at the end of the ray
            Gizmos.color = Color.red;

            // Draw the sphere at the end of the ray path to represent the SphereCast's detection area
            Gizmos.DrawWireSphere(origin + direction * rayLength, sphereRadius);
        }
    }
}
