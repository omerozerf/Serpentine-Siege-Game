using UnityEngine;

namespace _Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager ms_Instance;

        private int m_BulletDamage = 1;
        

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
    }
}