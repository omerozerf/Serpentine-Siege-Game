using System;
using UnityEngine;

namespace Players
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int SPEED = Animator.StringToHash("speed");
        
        
        [SerializeField] private Animator _animator;


        private Vector3 m_LastFramePosition;
        private float m_Speed;


        private void Awake()
        {
            m_LastFramePosition = transform.position;
        }


        private void Update()
        {
            var position = transform.position;
            var deltaPosition = position - m_LastFramePosition;

            var speed = Mathf.Abs(deltaPosition.z) > 0f ? 1f : 0f;

            m_Speed = Mathf.Lerp(m_Speed, speed, Time.deltaTime * 15f);
            
            SetSpeed(m_Speed);

            m_LastFramePosition = position;
        }


        private void SetSpeed(float speed)
        {
            _animator.SetFloat(SPEED, speed);
        }
    }
}