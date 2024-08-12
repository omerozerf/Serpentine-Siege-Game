using System;
using Players;
using UnityEngine;

public class SoldierFollowPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float m_DistanceX;
    private float m_DistanceZ;

    private void Start()
    {
        m_DistanceX = transform.position.x;
        m_DistanceZ = transform.position.z;
    }


    private void Update()
    {
        if (_player == null) return;
        
        Vector3 newPosition = transform.position;
        newPosition.x = _player.transform.position.x + m_DistanceX;
        newPosition.z = _player.transform.position.z + m_DistanceZ;

        transform.position = newPosition;
    }
}