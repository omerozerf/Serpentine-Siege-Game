using _Managers;
using UnityEngine;

namespace Bullets
{
    public class BulletMover : MonoBehaviour
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
    
    
        private void Update()
        {
            if(GameManager.GetIsPaused()) return;
            
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }
    }
}