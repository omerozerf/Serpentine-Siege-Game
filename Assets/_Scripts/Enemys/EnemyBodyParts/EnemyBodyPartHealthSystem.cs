using System;
using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPartHealthSystem : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explode;
        [SerializeField] private EnemyBodyPart _enemyBodyPart;
        [SerializeField] private int _health;
        
        public static event Action OnEnemyDied;
        
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
                var explode = Instantiate(_explode, transform.position, Quaternion.identity);

                _enemyBodyPart.DestroySelf();
                OnEnemyDied?.Invoke();

                var render = explode.GetComponent<ParticleSystemRenderer>();

                var mat = new MaterialPropertyBlock();
                
                mat.SetColor("_Color", _enemyBodyPart.GetColor());
                
                render.SetPropertyBlock(mat);
            }
        }

        
        public void SetHealth(int health)
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
