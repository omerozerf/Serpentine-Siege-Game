using System;
using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPartCollision : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart _enemyBodyPart;

        private float m_CheckTime;
        private bool isReversing = false;

        private void Update()
        {
            bool isBlocked = Physics.Raycast(transform.position, -transform.forward, 2.5f) ||
                             Physics.Raycast(transform.position, Quaternion.Euler(0, -30, 0) * -transform.forward, 2.5f) ||
                             Physics.Raycast(transform.position, Quaternion.Euler(0, 30, 0) * -transform.forward, 2.5f);

            // Adjust the check time based on whether the path is blocked or not
            if (!isBlocked)
            {
                m_CheckTime += Time.deltaTime;
            }
            else
            {
                m_CheckTime = 0; // Reset the check time when the path is blocked
            }

            // Reverse the speed after 1.5 seconds of having no obstacles
            if (m_CheckTime > 0.8f && !isReversing)
            {
                _enemyBodyPart.GetPathFollower().speed *= -1;
                isReversing = true; // Set reversing to true
            }
            else if (isBlocked && isReversing)
            {
                _enemyBodyPart.GetPathFollower().speed = Mathf.Abs(_enemyBodyPart.GetPathFollower().speed);
                isReversing = false; // Reset reversing flag
            }
        }

        public EnemyBodyPart GetEnemyBodyPart()
        {
            return _enemyBodyPart;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + -transform.forward * 2.5f);
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -30, 0) * -transform.forward * 2.5f);
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 30, 0) * -transform.forward * 2.5f);
        }
    }
}
