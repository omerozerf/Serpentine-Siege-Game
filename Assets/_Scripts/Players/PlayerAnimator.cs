using System;
using UnityEngine;

namespace Players
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int SPEED = Animator.StringToHash("speed");
        
        
        [SerializeField] private Animator _animator;


        private Vector3 m_LastFramePosition;


        private void Awake()
        {
            m_LastFramePosition = transform.position;
        }


        private void Update()
        {
            var position = transform.position;
            var deltaPosition = position - m_LastFramePosition;

            var speed = Mathf.Abs(deltaPosition.z) > 0f ? 1f : 0f;
            
            SetSpeed(speed);

            m_LastFramePosition = position;
        }


        private void SetSpeed(float speed)
        {
            _animator.SetFloat(SPEED, speed);
        }
    }
}