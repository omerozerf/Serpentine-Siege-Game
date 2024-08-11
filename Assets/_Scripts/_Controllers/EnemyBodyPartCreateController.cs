﻿using System;
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
    
        public event Action<EnemyBodyPart> OnEnemyBodyPartCreated;


        private async void Start()
        {
            for (int index = 0; index < 50; index++)
            {
                // await UniTask.WaitForSeconds(0.715f);
                CreateEnemyBodyPart(Vector3.one * 999, Quaternion.identity, _enemyBodyPartsParent, index);
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
    }
}