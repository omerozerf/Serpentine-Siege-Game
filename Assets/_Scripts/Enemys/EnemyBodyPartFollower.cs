using System.Collections.Generic;
using _Controllers;
using Enemys.EnemyBodyParts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemys
{
    public class EnemyBodyPartFollower : MonoBehaviour 
    {
        [FormerlySerializedAs("_enemyBodyPartCreator")] [SerializeField] private EnemyBodyPartCreateController _enemyBodyPartCreateController;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _steerSpeed = 180;
        [SerializeField] private float _bodySpeed = 5;
        [SerializeField] private int _gap = 10;
    
        private readonly List<EnemyBodyPart> m_EnemyBodyPartList = new ();
        private readonly List<Vector3> m_PositionsHistoryList = new ();


        private void Awake()
        {
            _enemyBodyPartCreateController.OnEnemyBodyPartCreated += OnEnemyBodyPartCreated;
        }
    
        private void Update()
        {
            BodyPartsUpdatePosition();
        }
    
        private void OnDestroy()
        {
            _enemyBodyPartCreateController.OnEnemyBodyPartCreated -= OnEnemyBodyPartCreated;
        }
    
    
        private void OnEnemyBodyPartCreated(EnemyBodyPart enemyBodyPart)
        {
            m_EnemyBodyPartList.Add(enemyBodyPart);
        }
    
    
        private void BodyPartsUpdatePosition()
        {
            m_PositionsHistoryList.Insert(0, transform.position);

            int index = 0;
            foreach (EnemyBodyPart enemyBodyPart in m_EnemyBodyPartList)
            {
                if (!enemyBodyPart) continue;
            
                Vector3 point = m_PositionsHistoryList[Mathf.Clamp(index * _gap, 0, m_PositionsHistoryList.Count - 1)];

                Vector3 moveDirection = point - enemyBodyPart.GetPosition();
                enemyBodyPart.transform.position += moveDirection * (_bodySpeed * Time.deltaTime);

                enemyBodyPart.transform.LookAt(point);

                index++;
            }
        }
    }
}