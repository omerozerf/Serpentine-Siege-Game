using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Managers
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<float> OnEnemySpeedChanged; 
        
        private static GameManager ms_Instance;
        
        private int m_BulletDamage = 1;
        private float m_EnemySpeed = 7;

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

        public static void AddEnemyMoveSpeed(float percentage)
        {
            if (percentage is < -100 or > 100)
            {
                Debug.LogWarning("Percentage must be between -100 and 100.");
                return;
            }

            float newMoveSpeed = ms_Instance.m_EnemySpeed * (1 + (percentage / 100f));

            ms_Instance.m_EnemySpeed = Mathf.Max(0.01f, newMoveSpeed);
            
            OnEnemySpeedChanged?.Invoke(ms_Instance.m_EnemySpeed);
        }
    }
}