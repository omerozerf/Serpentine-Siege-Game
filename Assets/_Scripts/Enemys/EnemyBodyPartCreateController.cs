using System;
using Enemys.EnemyBodyParts;
using UnityEngine;

namespace Enemys
{
    public class EnemyBodyPartCreateController : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart _enemyBodyPartPrefab;
        [SerializeField] private Transform _enemyBodyPartsParent;
    
        public event Action<EnemyBodyPart> OnEnemyBodyPartCreated;


        private void Start()
        {
            for (int index = 0; index < 50; index++)
            {
                CreateEnemyBodyPart(Vector3.zero, Quaternion.identity, _enemyBodyPartsParent, index);
            }
        }


        private EnemyBodyPart CreateEnemyBodyPart(Vector3 position, Quaternion rotation, Transform parent, int index)
        {
            EnemyBodyPart enemyBodyPart = Instantiate(_enemyBodyPartPrefab, position, rotation, parent);
            enemyBodyPart.name = $"Enemy Body Part - {index}";
            
            OnEnemyBodyPartCreated?.Invoke(enemyBodyPart);
        
            return enemyBodyPart;
        }
    }
}