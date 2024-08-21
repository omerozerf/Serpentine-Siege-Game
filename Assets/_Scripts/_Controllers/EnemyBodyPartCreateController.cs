using System;
using Cysharp.Threading.Tasks;
using Enemys.EnemyBodyParts;
using PathCreation;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Controllers
{
    public class EnemyBodyPartCreateController : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart[] _enemyBodyPartPrefabArray;
        [SerializeField] private Transform _enemyBodyPartsParent;
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private int _enemyBodyPartCount;
    
        public event Action<EnemyBodyPart> OnEnemyBodyPartCreated;
        public static event Action OnAllEnemyBodyPartsCreated;

        private static EnemyBodyPartCreateController ms_Instance;
        
        private void Awake()
        {
            ms_Instance = this;
        }

        private async void Start()
        {
            for (int index = 0; index < _enemyBodyPartCount; index++)
            {
                EnemyBodyPart enemyBodyPart = 
                    CreateEnemyBodyPart(_enemyBodyPartsParent.transform.position, Quaternion.identity,
                        _enemyBodyPartsParent, index);

                /*
                if (index == _enemyBodyPartCount - 2)
                {
                    enemyBodyPart.gameObject.SetActive(false);
                    enemyBodyPart.DestroySelf(5f);
                    Debug.Log(enemyBodyPart, enemyBodyPart);
                }*/

                if (index < 10)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(5, 10));
                }
                else if (index < 25)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(5, 15));
                }
                else if (index < 30)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(10, 25));
                }
                else if (index < 35)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(15, 25));
                }
                else if (index < 40)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(25, 30));
                }
                else if (index < 45)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(30, 38));
                }
                else if (index < 46)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(40, 45));
                }
                else if (index < 50)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(45, 50));
                }
                else if (index < 55)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(17, 19));
                }
                else if (index < 65)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(19, 21));
                }
                else if (index < 70)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(21, 23));
                }
                else if (index < 75)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(23, 25));
                }
                else if (index < 80)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(25, 27));
                }
                else if (index < 85)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(27, 29));
                }
                else if (index < 90)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(29, 31));
                }
                else if (index < 95)
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(31, 33));
                }
                else
                {
                    enemyBodyPart.GetEnemyBodyPartHealthSystem().SetHealth(Random.Range(33, 35));
                }
            }
            
            OnAllEnemyBodyPartsCreated?.Invoke();
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