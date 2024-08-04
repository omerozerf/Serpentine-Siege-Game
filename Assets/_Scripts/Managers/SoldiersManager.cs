using UnityEngine;

namespace Managers
{
    public class SoldiersManager : MonoBehaviour
    {
        [SerializeField] private Soldier[] _soldierArray;

        private static SoldiersManager ms_Instance;

        private void Awake()
        {
            ms_Instance = this;
        }

        public static SoldiersManager GetInstance()
        {
            return ms_Instance;
        }

        public Soldier[] GetSoldierArray()
        {
            return _soldierArray;
        }

        public Soldier GetSoldier(int index)
        {
            return _soldierArray[index];
        }

        public static void SetSoldierSetActive(int value)
        {
            if (value > 0)
            {
                int activatedCount = 0;
                for (int i = 0; i < ms_Instance._soldierArray.Length; i++)
                {
                    if (!ms_Instance._soldierArray[i].gameObject.activeSelf)
                    {
                        ms_Instance._soldierArray[i].gameObject.SetActive(true);
                        activatedCount++;
                        if (activatedCount >= value)
                        {
                            break;
                        }
                    }
                }
            }
            else if (value < 0)
            {
                int absValue = Mathf.Abs(value);
                int deactivatedCount = 0;
                for (int i = ms_Instance._soldierArray.Length - 1; i >= 0; i--)
                {
                    if (ms_Instance._soldierArray[i].gameObject.activeSelf)
                    {
                        ms_Instance._soldierArray[i].gameObject.SetActive(false);
                        deactivatedCount++;
                        if (deactivatedCount >= absValue)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public static int GetActiveSoldierCount()
        {
            int count = 0;
            foreach (Soldier soldier in ms_Instance._soldierArray)
            {
                if (soldier.gameObject.activeSelf)
                {
                    count++;
                }
            }

            return count;
        }
    }
}