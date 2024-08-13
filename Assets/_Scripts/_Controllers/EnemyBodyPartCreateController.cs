using System;
using Cysharp.Threading.Tasks;
using Enemys.EnemyBodyParts;
using PathCreation;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Controllers
{
    public class EnemyBodyPartCreateController : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart[] _enemyBodyPartPrefabArray;
        [SerializeField] private Transform _enemyBodyPartsParent;
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private int _enemyBodyPartCount;
    
        public event Action<EnemyBodyPart> OnEnemyBodyPartCreated;

        private static EnemyBodyPartCreateController ms_Instance;
        
        private void Awake()
        {
            ms_Instance = this;
        }

        private async void Start()
        {
            for (int index = 0; index < _enemyBodyPartCount; index++)
            {
                // await UniTask.WaitForSeconds(0.715f);
                CreateEnemyBodyPart(Vector3.one, Quaternion.identity, _enemyBodyPartsParent, index);
            }
        }


        private EnemyBodyPart CreateEnemyBodyPart(Vector3 position, Quaternion rotation, Transform parent, int index)
        {
            EnemyBodyPart enemyBodyPart = Instantiate(GetRandomEnemyBodyPartPrefab(), position, rotation, parent);
            enemyBodyPart.SetPathCreator(_pathCreator);
            enemyBodyPart.name = $"Enemy Body Part - {index}";
            
            OnEnemyBodyPartCreated?.Invoke(enemyBodyPart);
        
            return enemyBodyPart;
        }
        
        private EnemyBodyPart GetRandomEnemyBodyPartPrefab()
        {
            return _enemyBodyPartPrefabArray[UnityEngine.Random.Range(0, _enemyBodyPartPrefabArray.Length)];
        }
        
        public static int GetEnemyBodyPartCount()
        {
            return ms_Instance._enemyBodyPartCount;
        }
    }
}