using System.Collections;
using _Managers;
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
            if(GameManager.GetIsPaused()) return;
            
            Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        }

        
        public void AddFireRate(float percentage)
        {
            if (percentage is < -100 or > 100)
            {
                Debug.LogWarning("Percentage must be between -100 and 100.");
                return;
            }

            float newFireRate = _fireRate * (1 + (percentage / 100f));

            _fireRate = Mathf.Max(0.01f, newFireRate);
        }
    }
}