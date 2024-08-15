using System;
using System.Collections;
using _Managers;
using Bullets;
using Cysharp.Threading.Tasks;
using Players;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    private float m_FireRate;

    private void Awake()
    {
        GameManager.OnFireRateChanged += OnFireRateChanged;
        
    }

    private async void Start()
    {
        await UniTask.DelayFrame(1);
        
        m_FireRate = GameManager.GetFireRate();
        StartCoroutine(ShootRoutine());
    }

    private void OnDestroy()
    {
        GameManager.OnFireRateChanged -= OnFireRateChanged;
    }
    
    
    private void OnFireRateChanged(float obj)
    {
        m_FireRate = obj;
    }
    

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(GameManager.GetFireRate());
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void Shoot()
    {
        if(GameManager.GetIsPaused() || Player.GetPlayerFollowSnake().GetFirstFollow()) return;
            
        Bullet bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
    }

        
    public void AddFireRate(float percentage)
    {
        if (percentage is < -100 or > 100)
        {
            Debug.LogWarning("Percentage must be between -100 and 100.");
            return;
        }

        float newFireRate = m_FireRate * (1 + (percentage / 100f));

        m_FireRate = Mathf.Max(0.01f, newFireRate);
    }
}