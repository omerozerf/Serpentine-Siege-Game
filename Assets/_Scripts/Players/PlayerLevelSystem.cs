using System;
using UnityEngine;

namespace Players
{
    public class PlayerLevelSystem : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        public static event Action<int> OnLevelUp; 
        
        [SerializeField] private int m_Level = 1;
        [SerializeField] private int m_Experience = 0;
        [SerializeField] private int m_ExperienceToNextLevel = 100; // İlk level için gerekli deneyim puanı
        [SerializeField] private float experienceMultiplier = 1.5f; // Her levelde gerekli deneyim artış katsayısı
        
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
            m_ExperienceToNextLevel = Mathf.RoundToInt(m_ExperienceToNextLevel * experienceMultiplier); // Her levelde gerekli deneyimi arttır
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