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
            Debug.Log(IsPathBlocked());
        }


        public EnemyBodyPart GetEnemyBodyPart()
        {
            return _enemyBodyPart;
        }

        public bool IsPathBlocked()
        {
            return Physics.Raycast(transform.position, -transform.forward, 2.5f) ||
                   Physics.Raycast(transform.position, Quaternion.Euler(0, -35, 0) * -transform.forward, 2.5f) ||
                   Physics.Raycast(transform.position, Quaternion.Euler(0, 35, 0) * -transform.forward, 2.5f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, transform.position + -transform.forward * 2.5f);
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -35, 0) * -transform.forward * 2.5f);
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 35, 0) * -transform.forward * 2.5f);
        }
    }
}
