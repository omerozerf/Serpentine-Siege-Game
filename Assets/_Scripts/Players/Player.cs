using Enemys;
using UnityEngine;
using UnityEngine.Serialization;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerFollowSnake _playerFollowSnake;
        [FormerlySerializedAs("_playerMovement")] [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private PlayerLevelSystem _playerLevelSystem;
        [FormerlySerializedAs("_playerBulletShooter")] [SerializeField] private BulletShooter _bulletShooter;
        
        private static Player ms_Instance;
        
        
        private void Awake()
        {
            ms_Instance = this;
        }
        
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
        
        public static PlayerFollowSnake GetPlayerFollowSnake()
        {
            return ms_Instance._playerFollowSnake;
        }
    }
}
