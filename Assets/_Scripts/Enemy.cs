using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyBodyPartFollower _enemyBodyPartFollower;
}