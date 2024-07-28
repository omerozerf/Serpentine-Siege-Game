using UnityEngine;
using UnityEngine.Serialization;

namespace Enemys
{
    public class Enemy : MonoBehaviour
    {
        [FormerlySerializedAs("_enemyMovement")] [SerializeField] private EnemyMover _enemyMover;
        [FormerlySerializedAs("_enemyBodyPartFollower")] [SerializeField] private EnemyBodyPartFollowManager _enemyBodyPartFollowManager;
    }
}