using System;
using _Managers;
using Enemys;
using Enemys.EnemyBodyParts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bullets
{
    public class BulletCollision : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private LayerMask _collisionMask;
        [SerializeField] private ParticleSystem _boomParticle;
        
        private readonly Collider[] m_ResultColliderArray = new Collider[1];

        private void Update()
        {
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            Vector3 point1 = _capsuleCollider.transform.position + _capsuleCollider.transform.up *
                (_capsuleCollider.height / 2 - _capsuleCollider.radius);
            Vector3 point2 = _capsuleCollider.transform.position - _capsuleCollider.transform.up *
                (_capsuleCollider.height / 2 - _capsuleCollider.radius);

            int size = Physics.OverlapCapsuleNonAlloc(point1, point2, _capsuleCollider.radius, m_ResultColliderArray, _collisionMask);

            foreach (Collider hitCollider in m_ResultColliderArray)
            {
                if (hitCollider) HandleCollision(hitCollider);
            }
        }

        private void HandleCollision(Collider hitCollider)
        {
            EnemyBodyPartCollision enemyBodyPartCollision  = hitCollider.GetComponent<EnemyBodyPartCollision>();
            
            enemyBodyPartCollision.OnShoot(GameManager.GetBulletDamage());
            
            _boomParticle.transform.SetParent(null);
            _boomParticle.Play();
            
            Destroy(_bullet.gameObject);
        }
        
        
        public Bullet GetBullet()
        {
            return _bullet;
        }
    }
}