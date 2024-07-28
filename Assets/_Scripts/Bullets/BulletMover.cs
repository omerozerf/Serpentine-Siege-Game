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
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }
    }
}