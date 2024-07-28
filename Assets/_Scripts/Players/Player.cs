using Enemys;
using UnityEngine;
using UnityEngine.Serialization;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [FormerlySerializedAs("_playerMovement")] [SerializeField] private PlayerMover _playerMover;
    }
}
