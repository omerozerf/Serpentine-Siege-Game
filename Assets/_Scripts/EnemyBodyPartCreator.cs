using System;
using UnityEngine;

public class EnemyBodyPartCreator : MonoBehaviour
{
    [SerializeField] private EnemyBodyPart _enemyBodyPartPrefab;
    [SerializeField] private Transform _enemyBodyPartsParent;
    
    public event Action<EnemyBodyPart> OnEnemyBodyPartCreated;


    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            CreateEnemyBodyPart(Vector3.zero, Quaternion.identity, _enemyBodyPartsParent);
        }
    }


    private EnemyBodyPart CreateEnemyBodyPart(Vector3 position, Quaternion rotation, Transform parent)
    {
        EnemyBodyPart enemyBodyPart = Instantiate(_enemyBodyPartPrefab, position, rotation, parent);
        OnEnemyBodyPartCreated?.Invoke(enemyBodyPart);
        
        return enemyBodyPart;
    }
}