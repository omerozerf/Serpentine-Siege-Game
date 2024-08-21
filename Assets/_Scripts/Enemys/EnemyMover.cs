using UnityEngine;

namespace Enemys
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _turnSpeed = 100;
        [SerializeField] private float _turnInterval = 1;

        private float m_NextTurnTime;   
        private Quaternion m_TargetRotation;
        private int m_Flag;

        private void Start()
        {
            SetRandomRotation();           
        }

        void Update()
        {
            MoveForward();                 
            HandleTurning();               
        }

        void MoveForward()
        {
            transform.Translate(Vector3.forward * (_moveSpeed * Time.deltaTime));
        }

        void HandleTurning()
        {
            if (Time.time >= m_NextTurnTime)
            {
                SetRandomRotation();
                m_NextTurnTime = Time.time + _turnInterval;
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_TargetRotation, _turnSpeed * Time.deltaTime);
        }

        void SetRandomRotation()
        {
            // Y ekseni etrafında -90 ile 90 derece arasında rastgele bir açı belirle
            float randomYRotation;
            if (m_Flag % 2 == 0)
            { 
                randomYRotation = 270;
            }
            else
            {
                randomYRotation = 90;
            }
            m_Flag++;
        
            m_TargetRotation = Quaternion.Euler(0, randomYRotation, 0);
        }
    }
}