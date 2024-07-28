using System.Collections.Generic;
using _Controllers;
using Enemys.EnemyBodyParts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemys
{
    public class EnemyBodyPartFollowManager : MonoBehaviour 
    {
        [SerializeField] private EnemyBodyPartCreateController _enemyBodyPartCreateController;

        private static EnemyBodyPartFollowManager ms_Instance;
        
        private readonly List<EnemyBodyPart> m_EnemyBodyPartList = new();
        private readonly List<Vector3> m_PositionsHistoryList = new();

        private void Awake()
        {
            ms_Instance = this;
            _enemyBodyPartCreateController.OnEnemyBodyPartCreated += OnEnemyBodyPartCreated;
        }
    
        private void OnDestroy()
        {
            _enemyBodyPartCreateController.OnEnemyBodyPartCreated -= OnEnemyBodyPartCreated;
        }
    
        private void OnEnemyBodyPartCreated(EnemyBodyPart enemyBodyPart)
        {
            m_EnemyBodyPartList.Add(enemyBodyPart);
        }

        public static void HitBodyPart(EnemyBodyPart hitBodyPart)
        {
            int hitIndex = ms_Instance.m_EnemyBodyPartList.IndexOf(hitBodyPart);
            if (hitIndex == -1)
            {
                Debug.LogWarning("Hit body part not found in the list.");
                return;
            }

            for (int i = 0; i < hitIndex; i++)
            {
                EnemyBodyPart bodyPart = ms_Instance.m_EnemyBodyPartList[i];

                bodyPart.GetPathFollower().speed *= -1;
            }
        }
    }
}