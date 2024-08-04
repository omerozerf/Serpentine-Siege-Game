using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Managers
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<float> OnEnemySpeedChanged;
        public static event Action<float> OnFireRateChanged; 
        
        private static GameManager ms_Instance;
        
        private int m_BulletDamage = 1;
        [SerializeField] private float m_EnemySpeed = 7;
        [SerializeField] private float m_FireRate = 0.5f;
        private bool m_IsPaused;

        private void Awake()
        {
            ms_Instance = this;
        }

        public static GameManager GetInstance()
        {
            return ms_Instance;
        }
        
        public static int GetBulletDamage()
        {
            return ms_Instance.m_BulletDamage;
        }

        public static void SetBulletDamage(int bulletDamage)
        {
            ms_Instance.m_BulletDamage = bulletDamage;
        }
        
        public static float GetEnemySpeed()
        {
            return ms_Instance.m_EnemySpeed;
        }

        public static void SetEnemyMoveSpeed(float speed)
        {
            ms_Instance.m_EnemySpeed = speed;
            OnEnemySpeedChanged?.Invoke(speed);
        }
        
        public static void SetIsPaused(bool isPaused)
        {
            ms_Instance.m_IsPaused = isPaused;
        }

        public static bool GetIsPaused()
        {
            return ms_Instance.m_IsPaused;
        }
        
        public static float GetFireRate()
        {
            return ms_Instance.m_FireRate;
        }

        public static void SetFireRate(float fireRate)
        {
            ms_Instance.m_FireRate = fireRate;
            OnFireRateChanged?.Invoke(fireRate);
        }
    }
}