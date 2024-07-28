using System.Collections;
using System.Collections.Generic;
using _Controllers;
using Enemys.EnemyBodyParts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemys
{
    public class EnemyBodyPartFollowManager : MonoBehaviour
    {
        [SerializeField] private EnemyBodyPartCreateController _enemyBodyPartCreateController;

        private static EnemyBodyPartFollowManager ms_Instance;

        private readonly List<EnemyBodyPart> m_EnemyBodyPartList = new();
        private readonly List<Vector3> m_PositionsHistoryList = new();

        private void Awake()
        {
            ms_Instance = this;
            _enemyBodyPartCreateController.OnEnemyBodyPartCreated += OnEnemyBodyPartCreated;
        }

        private void OnDestroy()
        {
            _enemyBodyPartCreateController.OnEnemyBodyPartCreated -= OnEnemyBodyPartCreated;
        }

        private void OnEnemyBodyPartCreated(EnemyBodyPart enemyBodyPart)
        {
            m_EnemyBodyPartList.Add(enemyBodyPart);
        }

        public static void HitBodyPart(EnemyBodyPart hitBodyPart)
        {
            hitBodyPart.DestroySelf();
            
            int hitIndex = ms_Instance.m_EnemyBodyPartList.IndexOf(hitBodyPart);
            if (hitIndex == -1)
            {
                Debug.LogWarning("Hit body part not found in the list.");
                return;
            }

            for (int i = 0; i < hitIndex; i++)
            {
                EnemyBodyPart bodyPart = ms_Instance.m_EnemyBodyPartList[i];
                bodyPart.GetPathFollower().speed *= -1;
            }

            if (hitIndex > 0)
            {
                ms_Instance.StartCoroutine(ms_Instance.CheckPreviousBodyPartBlocked(hitIndex));
            }
        }

        private IEnumerator CheckPreviousBodyPartBlocked(int hitIndex)
        {
            if (hitIndex <= 0 || hitIndex >= m_EnemyBodyPartList.Count)
            {
                Debug.LogWarning("Invalid hit index or out of range: " + hitIndex);
                yield break;
            }

            EnemyBodyPart previousBodyPart = m_EnemyBodyPartList[hitIndex - 1];
            if (previousBodyPart == null)
            {
                Debug.LogError("Previous body part is null at index: " + (hitIndex - 1));
                yield break;
            }

            EnemyBodyPartCollision previousBodyPartCollision = previousBodyPart.GetEnemyBodyPartCollision();
            if (previousBodyPartCollision == null)
            {
                Debug.LogError("EnemyBodyPartCollision component is missing on previous body part at index: " +
                               (hitIndex - 1));
                yield break;
            }

            for (int i = 0; i < hitIndex; i++)
            {
                EnemyBodyPart bodyPart = m_EnemyBodyPartList[i];
                if (bodyPart == null)
                {
                    Debug.LogError("Body part is null at index: " + i);
                    continue;
                }

                var pathFollower = bodyPart.GetPathFollower();
                if (pathFollower == null)
                {
                    Debug.LogError("PathFollower component is missing on body part at index: " + i);
                    continue;
                }

                pathFollower.speed = Mathf.Abs(pathFollower.speed);
            }
        }
    }
}
