using System;
using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPartCollision : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart _enemyBodyPart;
        [SerializeField] private LayerMask _collisionMask;

        public event Action OnShooted;
        
        private float m_CheckTime;
        private bool isReversing = false;

        private void Update()
        {
            Debug.Log(IsPathBlocked() + " " + _enemyBodyPart.name);
        }

        public EnemyBodyPart GetEnemyBodyPart()
        {
            return _enemyBodyPart;
        }
        
        public void OnShoot()
        {
            OnShooted?.Invoke();
        }

        public bool IsPathBlocked()
        {
            RaycastHit hit;
            // SphereCast yaparak belirli bir mesafede çakışma olup olmadığını kontrol et
            if (Physics.SphereCast(transform.position, 1.5f, -transform.forward, out hit, 2.5f, _collisionMask, QueryTriggerInteraction.Ignore))
            {
                // Çakışan nesne kendimiz değilse true döndür
                if (hit.collider.gameObject != gameObject)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Vector3 direction = -transform.forward * 2.5f;
            Vector3 origin = transform.position;

            // Küre çizimi
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(origin + direction * 0.8f, 1.5f);
        }
    }
}