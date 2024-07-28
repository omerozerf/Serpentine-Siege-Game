using PathCreation;
using PathCreation.Examples;
using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPart : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPartCollision _enemyBodyPartCollision;
        [SerializeField] private PathFollower _pathFollower;
        
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }
        
        public EnemyBodyPartCollision GetEnemyBodyPartCollision()
        {
            return _enemyBodyPartCollision;
        }
        
        
        public void SetPathCreator(PathCreator pathCreator)
        {
            _pathFollower.pathCreator = pathCreator;
        }
        
        public PathFollower GetPathFollower()
        {
            return _pathFollower;
        }
    }
}