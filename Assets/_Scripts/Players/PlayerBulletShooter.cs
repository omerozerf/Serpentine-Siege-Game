using System.Collections;
using Bullets;
using UnityEngine;

namespace Players
{
    public class PlayerBulletShooter : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _fireRate;

        
        private void Start()
        {
            StartCoroutine(ShootRoutine());
        }

        
        private IEnumerator ShootRoutine()
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(_fireRate);
            }
            // ReSharper disable once IteratorNeverReturns
        }
        
        private void Shoot()
        {
            Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        }
    }
}