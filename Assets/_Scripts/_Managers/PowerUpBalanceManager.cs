using System;
using Managers;
using NaughtyAttributes;
using Players;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Managers
{
    public class PowerUpBalanceManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private LevelUpPowerUpSO[] _levelUpPowerUpSoArray;

        public float MinFireRate = 0.25f;
        public float MaxFireRate = 2.0f;
        public float MinDamage = 1.0f;
        public float MaxDamage = 5.0f;
        public float MinMovementSpeed = 2.0f;
        public float MaxMovementSpeed = 8.0f;
        public float MinEnemySpeed = 1.0f;
        public float MaxEnemySpeed = 7.5f;
        public int MinSoldierCount = 0;
        public int MaxSoldierCount = 4;

        private static PowerUpBalanceManager ms_Instance;
        
        private float chanceFactor = 0.2f; // %20 şans
        private float maxPercentageChange = 10f; // Maksimum %10 değişim


        private void Awake()
        {
            ms_Instance = this;
        }

        private void Start()
        {
            CheckPowerUp();
        }

        private void SetFireRate(float value, float percentageChange)
        {
            foreach (var VARIABLE in _levelUpPowerUpSoArray)
            {
                if (VARIABLE.GetPowerUpType() == LevelUpPowerUpType.FireRate)
                {
                    VARIABLE.SetPowerUpValue((int) percentageChange);
                }
            }
            
            // GameManager.SetFireRate(value);
        }

        private void SetDamage(float value, float percentageChange)
        {
            
            foreach (var VARIABLE in _levelUpPowerUpSoArray)
            {
                if (VARIABLE.GetPowerUpType() == LevelUpPowerUpType.Damage)
                {
                    VARIABLE.SetPowerUpValue((int) value);
                }
            }
            
        }

        private void SetMovementSpeed(float value, float percentageChange)
        {
            
            foreach (var VARIABLE in _levelUpPowerUpSoArray)
            {
                if (VARIABLE.GetPowerUpType() == LevelUpPowerUpType.MovementSpeed)
                {
                    VARIABLE.SetPowerUpValue((int) percentageChange);
                }
            }
            
        }
        
        private void SetEnemySpeed(float value, float percentageChange)
        {
            foreach (var VARIABLE in _levelUpPowerUpSoArray)
            {
                if (VARIABLE.GetPowerUpType() == LevelUpPowerUpType.EnemySpeed)
                {
                    VARIABLE.SetPowerUpValue((int) percentageChange);
                }
            }
            
        }
        
        private void SetSoldierCount(int value, int percentageChange)
        {
            
            foreach (var VARIABLE in _levelUpPowerUpSoArray)
            {
                if (VARIABLE.GetPowerUpType() == LevelUpPowerUpType.SoldierCount)
                {
                    VARIABLE.SetPowerUpValue((int) value);
                }
            }
            
        }

        private float GetFireRate()
        {
            return GameManager.GetFireRate();
        }

        private float GetDamage()
        {
            return GameManager.GetBulletDamage();
        }

        private float GetMovementSpeed()
        {
            return _player.GetPlayerMover().GetMoveSpeed();
        }

        private float GetEnemySpeed()
        {
            return GameManager.GetEnemySpeed();
        }

        private int GetSoldierCount()
        {
            return SoldiersManager.GetActiveSoldierCount();
        }

        [Button]
        public static void CheckPowerUp()
        {
            ms_Instance.BalanceFireRate();
            ms_Instance.BalanceDamage();
            ms_Instance.BalanceMovementSpeed();
            ms_Instance.BalanceEnemySpeed();
            ms_Instance.BalanceSoldierCount();
        }

        private void BalanceFireRate()
        {
            int random = Random.Range(-15, 26);
            
            SetFireRate(0, random);
            
            return;
            
            float currentFireRate = GetFireRate();
            float percentageChange;
            float chance = Random.Range(0f, 1f); // 0 ile 1 arasında rastgele bir sayı

            float normalizedFireRate = Mathf.InverseLerp(MinFireRate, MaxFireRate, currentFireRate);

            if (normalizedFireRate <= 0.5f)
            {
                // Min değere daha yakınsa yukarı doğru hareket et
                percentageChange = (1 - normalizedFireRate) * 0.1f * 100; // Yüzdelik değişiklik
            }
            else
            {
                // Max değere daha yakınsa aşağı doğru hareket et
                percentageChange = (normalizedFireRate - 0.5f) * 0.1f * 100; // Yüzdelik değişiklik
            }

            if (chance < chanceFactor)
            {
                percentageChange *= Random.Range(0.8f, 1.2f); // Şans faktörü ile değişim
            }

            percentageChange = Mathf.Clamp(percentageChange, -maxPercentageChange, maxPercentageChange);

            float adjustment = (percentageChange / 100) * (MaxFireRate - MinFireRate);
            float newFireRate = Mathf.Clamp(currentFireRate + adjustment, MinFireRate, MaxFireRate);
            SetFireRate(newFireRate, percentageChange);
        }

        private void BalanceDamage()
        {
            int random = Random.Range(-1, 2);
            
            SetDamage(random, 0);
            
            return;
            
            float currentDamage = GetDamage();
            float percentageChange;
            float chance = Random.Range(0f, 1f); // 0 ile 1 arasında rastgele bir sayı

            float normalizedDamage = Mathf.InverseLerp(MinDamage, MaxDamage, currentDamage);

            if (normalizedDamage <= 0.5f)
            {
                percentageChange = (1 - normalizedDamage) * 0.1f * 100; // Yüzdelik değişiklik
            }
            else
            {
                percentageChange = (normalizedDamage - 0.5f) * 0.1f * 100; // Yüzdelik değişiklik
            }

            if (chance < chanceFactor)
            {
                percentageChange *= Random.Range(0.8f, 1.2f); // Şans faktörü ile değişim
            }

            percentageChange = Mathf.Clamp(percentageChange, -maxPercentageChange, maxPercentageChange);

            float adjustment = (percentageChange / 100) * (MaxDamage - MinDamage);
            float newDamage = Mathf.Clamp(currentDamage + adjustment, MinDamage, MaxDamage);
            SetDamage(newDamage, percentageChange);
        }

        private void BalanceMovementSpeed()
        {
            int random = Random.Range(-35, 16);
            
            SetMovementSpeed(0, random);
            
            return;
            
            float currentMovementSpeed = GetMovementSpeed();
            float percentageChange;
            float chance = Random.Range(0f, 1f); // 0 ile 1 arasında rastgele bir sayı

            float normalizedMovementSpeed = Mathf.InverseLerp(MinMovementSpeed, MaxMovementSpeed, currentMovementSpeed);

            if (normalizedMovementSpeed <= 0.5f)
            {
                percentageChange = (1 - normalizedMovementSpeed) * 0.1f * 100; // Yüzdelik değişiklik
            }
            else
            {
                percentageChange = (normalizedMovementSpeed - 0.5f) * 0.1f * 100; // Yüzdelik değişiklik
            }

            if (chance < chanceFactor)
            {
                percentageChange *= Random.Range(0.8f, 1.2f); // Şans faktörü ile değişim
            }

            percentageChange = Mathf.Clamp(percentageChange, -maxPercentageChange, maxPercentageChange);

            float adjustment = (percentageChange / 100) * (MaxMovementSpeed - MinMovementSpeed);
            float newMovementSpeed = Mathf.Clamp(currentMovementSpeed + adjustment, MinMovementSpeed, MaxMovementSpeed);
            SetMovementSpeed(newMovementSpeed, percentageChange);
        }

        private void BalanceEnemySpeed()
        {
            int random = Random.Range(-15, 26);
            
            SetEnemySpeed(0, random);
            
            return;
            
            float currentEnemySpeed = GetEnemySpeed();
            float percentageChange;
            float chance = Random.Range(0f, 1f); // 0 ile 1 arasında rastgele bir sayı

            float normalizedEnemySpeed = Mathf.InverseLerp(MinEnemySpeed, MaxEnemySpeed, currentEnemySpeed);

            if (normalizedEnemySpeed <= 0.5f)
            {
                percentageChange = (1 - normalizedEnemySpeed) * 0.1f * 100; // Yüzdelik değişiklik
            }
            else
            {
                percentageChange = (normalizedEnemySpeed - 0.5f) * 0.1f * 100; // Yüzdelik değişiklik
            }

            if (chance < chanceFactor)
            {
                percentageChange *= Random.Range(0.8f, 1.2f); // Şans faktörü ile değişim
            }

            percentageChange = Mathf.Clamp(percentageChange, -maxPercentageChange, maxPercentageChange);

            float adjustment = (percentageChange / 100) * (MaxEnemySpeed - MinEnemySpeed);
            float newEnemySpeed = Mathf.Clamp(currentEnemySpeed + adjustment, MinEnemySpeed, MaxEnemySpeed);
            SetEnemySpeed(newEnemySpeed, percentageChange);
        }

        private void BalanceSoldierCount()
        {
            int random = Random.Range(-1, 2);
            
            SetSoldierCount(random, 0);
            
            return;
            
            int currentSoldierCount = GetSoldierCount();
            int percentageChange;
            float chance = Random.Range(0f, 1f); // 0 ile 1 arasında rastgele bir sayı

            if (currentSoldierCount <= (MinSoldierCount + MaxSoldierCount) / 2)
            {
                percentageChange = (int)((1 - (float)(currentSoldierCount - MinSoldierCount) / (MaxSoldierCount - MinSoldierCount)) * 0.1f * 100); // Yüzdelik değişiklik
            }
            else
            {
                percentageChange = (int)(((float)(currentSoldierCount - MinSoldierCount) / (MaxSoldierCount - MinSoldierCount) - 0.5f) * 0.1f * 100); // Yüzdelik değişiklik
            }

            if (chance < chanceFactor)
            {
                percentageChange = (int)(percentageChange * Random.Range(0.8f, 1.2f)); // Şans faktörü ile değişim
            }

            percentageChange = Mathf.Clamp(percentageChange, -10, 10); // Maksimum %10 değişim

            int adjustment = (int)((percentageChange / 100) * (MaxSoldierCount - MinSoldierCount));
            int newSoldierCount = Mathf.Clamp(currentSoldierCount + adjustment, MinSoldierCount, MaxSoldierCount);
            SetSoldierCount(newSoldierCount, percentageChange);
        }
    }
}
