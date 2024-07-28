using UnityEngine;
using UnityEngine.Serialization;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [FormerlySerializedAs("_bulletMovement")] [SerializeField] private BulletMover _bulletMover;
    }
}