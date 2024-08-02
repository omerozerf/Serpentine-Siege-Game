using Enemys;
using UnityEngine;
using UnityEngine.Serialization;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [FormerlySerializedAs("_playerMovement")] [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private PlayerLevelSystem _playerLevelSystem;
        [SerializeField] private PlayerBulletShooter _playerBulletShooter;
        
        public PlayerMover GetPlayerMover()
        {
            return _playerMover;
        }
        
        public PlayerLevelSystem GetPlayerLevelSystem()
        {
            return _playerLevelSystem;
        }
        
        public PlayerBulletShooter GetPlayerBulletShooter()
        {
            return _playerBulletShooter;
        }
    }
}
