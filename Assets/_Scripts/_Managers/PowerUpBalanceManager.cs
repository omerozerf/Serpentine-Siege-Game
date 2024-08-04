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

        private float chanceFactor = 0.2f; // %20 şans
        private float maxPercentageChange = 10f; // Maksimum %10 değişim

        private void Start()
        {
            CheckPowerUp();
        }

        private void SetFireRate(float value, float percentageChange)
        {
            GameManager.SetFireRate(value);
            Debug.Log($"FireRate ayarlandı: {value} (Yüzdelik Değişiklik: {percentageChange}%)");
        }

        private void SetDamage(float value, float percentageChange)
        {
            GameManager.SetBulletDamage((int)value);
            Debug.Log($"Damage ayarlandı: {value} (Yüzdelik Değişiklik: {percentageChange}%)");
        }

        private void SetMovementSpeed(float value, float percentageChange)
        {
            _player.GetPlayerMover().AddMoveSpeed(value);
            Debug.Log($"MovementSpeed ayarlandı: {value} (Yüzdelik Değişiklik: {percentageChange}%)");
        }
        
        private void SetEnemySpeed(float value, float percentageChange)
        {
            GameManager.SetEnemyMoveSpeed(value);
            Debug.Log($"EnemySpeed ayarlandı: {value} (Yüzdelik Değişiklik: {percentageChange}%)");
        }
        
        private void SetSoldierCount(int value, int percentageChange)
        {
            SoldiersManager.SetSoldierSetActive(value);
            Debug.Log($"SoldierCount ayarlandı: {value} (Yüzdelik Değişiklik: {percentageChange}%)");
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
        private void CheckPowerUp()
        {
            BalanceFireRate();
            BalanceDamage();
            BalanceMovementSpeed();
            BalanceEnemySpeed();
            BalanceSoldierCount();
        }

        private void BalanceFireRate()
        {
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
