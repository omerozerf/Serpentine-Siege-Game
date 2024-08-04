using Enemys;
using UnityEngine;
using UnityEngine.Serialization;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [FormerlySerializedAs("_playerMovement")] [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private PlayerLevelSystem _playerLevelSystem;
        [FormerlySerializedAs("_playerBulletShooter")] [SerializeField] private BulletShooter _bulletShooter;
        
        public PlayerMover GetPlayerMover()
        {
            return _playerMover;
        }
        
        public PlayerLevelSystem GetPlayerLevelSystem()
        {
            return _playerLevelSystem;
        }
        
        public BulletShooter GetPlayerBulletShooter()
        {
            return _bulletShooter;
        }
    }
}
