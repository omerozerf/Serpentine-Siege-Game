using System;
using _Managers;
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

        public static event Action<EnemyBodyPart> OnIsHeadDead; 

        private bool m_HasMinusSpeed;

        private void Awake()
        {
            GameManager.OnEnemySpeedChanged += OnEnemySpeedChanged;
            _pathFollower.speed = GameManager.GetEnemySpeed();
        }

        private void OnDestroy()
        {
            GameManager.OnEnemySpeedChanged -= OnEnemySpeedChanged;
        }


        private void Update()
        {
            if (_isHead)
            {
                return;
            }
            
            if (!_enemyBodyPartCollision.IsPathBlocked() && !m_HasMinusSpeed)
            {
                _pathFollower.speed = -25;
                m_HasMinusSpeed = true;
            }
            if (_enemyBodyPartCollision.IsPathBlocked() && m_HasMinusSpeed)
            {
                _pathFollower.speed = GameManager.GetEnemySpeed();
                m_HasMinusSpeed = false;
            }
            
            Debug.Log(gameObject, gameObject);
            Debug.Log(_enemyBodyPartCollision.IsPathBlocked());
        }

        private void OnEnemySpeedChanged(float obj)
        {
            _pathFollower.speed = GameManager.GetEnemySpeed();
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
            GameManager.AddDeadEnemyCount();

            if (_isHead)
            {
                OnIsHeadDead?.Invoke(this);
            }
            
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
    }
}