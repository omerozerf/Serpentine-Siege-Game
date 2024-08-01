using System;
using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPartHealthSystem : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart _enemyBodyPart;
        [SerializeField] private int _health;
        
        public event Action<int> OnHealthChanged; 
        

        private void Awake()
        {
            _enemyBodyPart.GetEnemyBodyPartCollision().OnShooted += OnShooted;
        }
    
        private void OnDestroy()
        {
            _enemyBodyPart.GetEnemyBodyPartCollision().OnShooted -= OnShooted;
        }
    
    
        private void OnShooted(int damage)
        {
            TakeDamage(damage);
            if (_health <= 0)
            {
                _enemyBodyPart.DestroySelf();
            }
        }

        
        private void SetHealth(int health)
        {
            _health = health;
            
            OnHealthChanged?.Invoke(_health);
        }

        private void TakeDamage(int damage)
        {
            int newHealth = _health - damage;
            SetHealth(newHealth);
        }
        

        public int GetHealth()
        {
            return _health;
        }
    }
}
