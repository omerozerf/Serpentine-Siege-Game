﻿using System;
using System.Collections;
using _Controllers;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace _Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _winScreen;
        
        public static event Action<float> OnEnemySpeedChanged;
        public static event Action<float> OnFireRateChanged; 
        
        private static GameManager ms_Instance;
        
        private int m_BulletDamage = 1;
        [SerializeField] private float m_EnemySpeed = 7;
        [SerializeField] private float m_FireRate = 0.5f;
        private bool m_IsPaused;
        private int m_DeadEnemyCount;

        private void Awake()
        {
            ms_Instance = this;
            // SetIsPaused(true);
            Application.targetFrameRate = 60;
            
            EnemyBodyPartCreateController.OnAllEnemyBodyPartsCreated += OnAllEnemyBodyPartsCreated;
        }
        
        private void OnDestroy()
        {
            EnemyBodyPartCreateController.OnAllEnemyBodyPartsCreated -= OnAllEnemyBodyPartsCreated;
        }
        
        private async void OnAllEnemyBodyPartsCreated()
        {
            //             SetIsPaused(false);
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

            if (ms_Instance.m_BulletDamage < 1)
            {
                ms_Instance.m_BulletDamage = 1;
            }
        }
        
        public static float GetEnemySpeed()
        {
            return ms_Instance.m_EnemySpeed;
        }

        public static void SetEnemyMoveSpeed(float speed)
        {
            return;
            
            
            if (speed is < -100 or > 100)
            {
                Debug.LogWarning("Percentage must be between -100 and 100.");
                return;
            }

            float newEnemySpeed = ms_Instance.m_EnemySpeed * (1 + (speed / 100f));

            ms_Instance.m_EnemySpeed = Mathf.Max(0.01f, newEnemySpeed);
            OnEnemySpeedChanged?.Invoke(newEnemySpeed);
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

        public static void AddFireRate(float percentage)
        {
            if (percentage is < -100 or > 100)
            {
                Debug.LogWarning("Percentage must be between -100 and 100.");
                return;
            }

            float newFireRate = ms_Instance.m_FireRate * (1 + (percentage / 100f));

            ms_Instance.m_FireRate = Mathf.Max(0.01f, newFireRate);
        }
        
        public static void AddDeadEnemyCount()
        {
            ms_Instance.m_DeadEnemyCount++;

            if (ms_Instance.m_DeadEnemyCount == EnemyBodyPartCreateController.GetEnemyBodyPartCount())
            {
                ms_Instance._winScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}