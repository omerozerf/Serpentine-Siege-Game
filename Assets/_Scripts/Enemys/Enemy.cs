using UnityEngine;

namespace Enemys
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyBodyPartFollower _enemyBodyPartFollower;
    }
}