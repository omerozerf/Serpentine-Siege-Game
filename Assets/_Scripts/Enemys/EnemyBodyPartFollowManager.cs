using System;
using System.Collections;
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
        [SerializeField] private EnemyBodyPart _enemyBodyPartHead;
        
        private static EnemyBodyPartFollowManager ms_Instance;

        private readonly List<EnemyBodyPart> m_EnemyBodyPartList = new();
        private readonly List<Vector3> m_PositionsHistoryList = new();

        private void Awake()
        {
            ms_Instance = this;
            _enemyBodyPartCreateController.OnEnemyBodyPartCreated += OnEnemyBodyPartCreated;
        }

        private void Start()
        {
            m_EnemyBodyPartList.Add(_enemyBodyPartHead);
        }

        private void OnDestroy()
        {
            _enemyBodyPartCreateController.OnEnemyBodyPartCreated -= OnEnemyBodyPartCreated;
        }

        private void OnEnemyBodyPartCreated(EnemyBodyPart enemyBodyPart)
        {
            m_EnemyBodyPartList.Add(enemyBodyPart);
            int indexOf = m_EnemyBodyPartList.IndexOf(enemyBodyPart);
            
            EnemyBodyPart previousEnemyBodyPart = m_EnemyBodyPartList[indexOf - 1];
            previousEnemyBodyPart.SetIsHead(false);
        }
    }
}
