using System;
using Enemys.EnemyBodyParts;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Controllers
{
    public class EnemyBodyPartCreateController : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart[] _enemyBodyPartPrefabArray;
        [SerializeField] private Transform _enemyBodyPartsParent;
    
        public event Action<EnemyBodyPart> OnEnemyBodyPartCreated;


        private void Start()
        {
            for (int index = 0; index < 50; index++)
            {
                CreateEnemyBodyPart(Vector3.one * 999, Quaternion.identity, _enemyBodyPartsParent, index);
            }
        }


        private EnemyBodyPart CreateEnemyBodyPart(Vector3 position, Quaternion rotation, Transform parent, int index)
        {
            EnemyBodyPart enemyBodyPart = Instantiate(GetRandomEnemyBodyPartPrefab(), position, rotation, parent);
            enemyBodyPart.name = $"Enemy Body Part - {index}";
            
            OnEnemyBodyPartCreated?.Invoke(enemyBodyPart);
        
            return enemyBodyPart;
        }
        
        private EnemyBodyPart GetRandomEnemyBodyPartPrefab()
        {
            return _enemyBodyPartPrefabArray[UnityEngine.Random.Range(0, _enemyBodyPartPrefabArray.Length)];
        }
    }
}