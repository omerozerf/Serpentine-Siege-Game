using System;
using UnityEngine;

namespace Players
{
    public class PlayerLevelSystem : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        public static event Action<int> OnLevelUp; 
        
        private int m_Level = 1;
        
        
        public void LevelUp()
        {
            m_Level++;
            
            OnLevelUp?.Invoke(m_Level);
        }
    }
}