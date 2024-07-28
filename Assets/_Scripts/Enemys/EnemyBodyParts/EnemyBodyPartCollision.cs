using UnityEngine;

namespace Enemys.EnemyBodyParts
{
    public class EnemyBodyPartCollision : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPart _enemyBodyPart;
        
        
        public EnemyBodyPart GetEnemyBodyPart()
        {
            return _enemyBodyPart;
        }
    }
}