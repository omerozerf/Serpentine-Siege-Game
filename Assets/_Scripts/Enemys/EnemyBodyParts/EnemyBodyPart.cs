using System;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPart : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPartCollision _enemyBodyPartCollision;
        [SerializeField] private EnemyBodyPartHealthSystem _enemyBodyPartHealthSystem;
        [SerializeField] private PathFollower _pathFollower;
        [SerializeField] private bool _isHead;

        private bool m_HasMinusSpeed;

        private void Update()
        {
            if (_isHead)
            {
                return;
            }
            
            if (!_enemyBodyPartCollision.IsPathBlocked() && !m_HasMinusSpeed)
            {
                _pathFollower.speed *= -1;
                m_HasMinusSpeed = true;
            }
            if (_enemyBodyPartCollision.IsPathBlocked() && m_HasMinusSpeed)
            {
                _pathFollower.speed = Mathf.Abs(_pathFollower.speed);
                m_HasMinusSpeed = false;
            }
        }


        public Vector3 GetPosition()
        {
            return transform.position;
        }
        
        public EnemyBodyPartCollision GetEnemyBodyPartCollision()
        {
            return _enemyBodyPartCollision;
        }
        
        
        public void SetPathCreator(PathCreator pathCreator)
        {
            _pathFollower.pathCreator = pathCreator;
        }
        
        public PathFollower GetPathFollower()
        {
            return _pathFollower;
        }

        public void DestroySelf(float delay = 0)
        {
            Destroy(gameObject, delay);
        }
        
        public void SetIsHead(bool isHead)
        {
            _isHead = isHead;
        }
        
        public EnemyBodyPartHealthSystem GetEnemyBodyPartHealthSystem()
        {
            return _enemyBodyPartHealthSystem;
        }
        
        public void AddMoveSpeed(int percentage)
        {
            if (percentage is < -100 or > 100)
            {
                Debug.LogWarning("Percentage must be between -100 and 100.");
                return;
            }

            float newMoveSpeed = _pathFollower.speed * (1 + (percentage / 100f));

            _pathFollower.speed = Mathf.Max(0.01f, newMoveSpeed);
        }
    }
}