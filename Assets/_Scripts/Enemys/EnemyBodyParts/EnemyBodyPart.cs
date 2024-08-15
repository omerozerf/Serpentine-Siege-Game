﻿using System;
using _Managers;
using PathCreation;
using PathCreation.Examples;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPart : MonoBehaviour
    {
        private static readonly int COLOR = Shader.PropertyToID("_Color");
        
        
        [SerializeField] private EnemyBodyPartCollision _enemyBodyPartCollision;
        [SerializeField] private EnemyBodyPartHealthSystem _enemyBodyPartHealthSystem;
        [SerializeField] private PathFollower _pathFollower;
        [SerializeField] private bool _isHead;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private bool _isMain;
        [SerializeField] private Color[] _colors;
        [SerializeField] private Renderer _renderer;
        

        public static event Action<EnemyBodyPart> OnIsHeadDead; 

        private Color m_Color;
        private bool m_HasMinusSpeed;
        private bool m_IsDead;

        private void Awake()
        {
            if (_renderer)
            {
                m_Color = _colors[Random.Range(0, _colors.Length)];
                var propertyBlock = new MaterialPropertyBlock();
                propertyBlock.SetColor(COLOR, m_Color);
                _renderer.SetPropertyBlock(propertyBlock);
            }

            if (!_isMain) _enemyBodyPartHealthSystem.OnHealthChanged += OnHealthChanged;

            if (!_isMain)
            {
                //_healthText.color = m_Color;
            }
        }

        private void Start()
        {
            GameManager.OnEnemySpeedChanged += OnEnemySpeedChanged;
            _pathFollower.speed = GameManager.GetEnemySpeed();
        }

        private void OnDestroy()
        {
            GameManager.OnEnemySpeedChanged -= OnEnemySpeedChanged;
            if (!_isMain) _enemyBodyPartHealthSystem.OnHealthChanged -= OnHealthChanged;
        }


        private void Update()
        {
            if (_isHead)
            {
                return;
            }
            
            if (!_enemyBodyPartCollision.IsPathBlocked() && !m_HasMinusSpeed)
            {
                _pathFollower.speed = -15;
                m_HasMinusSpeed = true;
            }
            if (_enemyBodyPartCollision.IsPathBlocked() && m_HasMinusSpeed)
            {
                _pathFollower.speed = GameManager.GetEnemySpeed();
                m_HasMinusSpeed = false;
            }
        }
        
        private void OnHealthChanged(int health)
        {
            _healthText.text = health.ToString();
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
            if (m_IsDead) return;
            m_IsDead = true;
         
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

        public Color GetColor()
        {
            return m_Color;
        }
    }
}