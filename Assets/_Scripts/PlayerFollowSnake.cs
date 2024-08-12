using System.Collections;
using Cysharp.Threading.Tasks;
using Enemys.EnemyBodyParts;
using UnityEngine;

public class PlayerFollowSnake : MonoBehaviour
{
    [SerializeField] private float _offSet;
    [SerializeField] private EnemyBodyPart _enemyBodyPartHead;
    [SerializeField] private float _speed;
    
    private float m_LastHeadZ;
    private bool m_IsFollowing;

    private async void Start()
    {
        await UniTask.DelayFrame(1);
        
        float headZ = _enemyBodyPartHead.transform.position.z;
        float playerZ = transform.position.z;
        float zDistance = headZ - playerZ;
        
        m_LastHeadZ = headZ;

        Debug.Log($"Start: HeadZ={headZ}, PlayerZ={playerZ}, ZDistance={zDistance}");

        if (zDistance > _offSet)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, headZ - _offSet);

            Debug.Log($"Start: Target Position={targetPos}");

            StartCoroutine(SmoothMove(targetPos));
        }
    }

    private void Update()
    {
        if (!m_IsFollowing) return;
        
        float currentHeadZ = _enemyBodyPartHead.transform.position.z;
        float distanceBetweenHeads = currentHeadZ - m_LastHeadZ;
        
        Debug.Log($"Update: CurrentHeadZ={currentHeadZ}, LastHeadZ={m_LastHeadZ}, DistanceBetweenHeads={distanceBetweenHeads}");

        if (distanceBetweenHeads > -3.5 && m_IsFollowing)
        {
            m_IsFollowing = false;
            m_LastHeadZ = currentHeadZ;
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, currentHeadZ - _offSet);
            
            StartCoroutine(SmoothMove(targetPos));
        }
    }

    private IEnumerator SmoothMove(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, _speed * Time.deltaTime);
            
            yield return null;
        }

        m_IsFollowing = true;
    }
}
