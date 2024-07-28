using UnityEngine;
using UnityEngine.Serialization;

namespace Enemys
{
    public class Enemy : MonoBehaviour
    {
        [FormerlySerializedAs("_enemyMovement")] [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private EnemyBodyPartFollower _enemyBodyPartFollower;
    }
}