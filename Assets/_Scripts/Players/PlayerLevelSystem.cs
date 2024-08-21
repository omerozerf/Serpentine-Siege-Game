using System;
using Enemys.EnemyBodyParts;
using TMPro;
using UnityEngine;

namespace Players
{
    public class PlayerLevelSystem : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        public static event Action<int> OnLevelUp; 
        
        [SerializeField] private int m_Level = 1;
        [SerializeField] private int m_Experience = 0;
        [SerializeField] private int m_ExperienceToNextLevel = 100; 
        [SerializeField] private float experienceMultiplier = 1.5f;
        [SerializeField] private TMP_Text _levelText;


        private void Awake()
        {
            EnemyBodyPartHealthSystem.OnEnemyDied += OnEnemyDied;
            _levelText.text = $"LVL<br>{m_Level}";
        }
        
        private void OnDestroy()
        {
            EnemyBodyPartHealthSystem.OnEnemyDied -= OnEnemyDied;
        }
        
        
        private void OnEnemyDied()
        {
            GainExperience(1);
        }


        public void GainExperience(int amount)
        {
            m_Experience += amount;
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            while (m_Experience >= m_ExperienceToNextLevel)
            {
                m_Experience -= m_ExperienceToNextLevel;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            m_Level++;
            m_ExperienceToNextLevel = Mathf.RoundToInt(m_ExperienceToNextLevel * experienceMultiplier);
            _levelText.text = $"LVL<br>{m_Level}";
            OnLevelUp?.Invoke(m_Level);
        }

        public int GetLevel()
        {
            return m_Level;
        }

        public int GetExperience()
        {
            return m_Experience;
        }

        public int GetExperienceToNextLevel()
        {
            return m_ExperienceToNextLevel;
        }

        public float GetExperienceMultiplier()
        {
            return experienceMultiplier;
        }
    }
}