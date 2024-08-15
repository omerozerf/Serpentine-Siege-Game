using System.Collections;
using Cysharp.Threading.Tasks;
using Enemys.EnemyBodyParts;
using Players;
using UnityEngine;

public class PlayerFollowSnake : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _offSet;
    [SerializeField] private EnemyBodyPart _enemyBodyPartHead;
    [SerializeField] private float _speed;
    
    private float m_LastHeadZ;
    private bool m_IsFollowing;
    private bool m_FirstFollow;

    private async void Start()
    {
        await UniTask.DelayFrame(1);
        
        m_FirstFollow = true;
        float headZ = _enemyBodyPartHead.transform.position.z;
        float playerZ = transform.position.z;
        float zDistance = headZ - playerZ;
        
        m_LastHeadZ = headZ;


        if (zDistance > _offSet)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, headZ - 25f);


            StartCoroutine(SmoothMove(targetPos));
        }
    }

    private void Update()
    {
        if (!m_IsFollowing) return;
        
        float currentHeadZ = _enemyBodyPartHead.transform.position.z;
        float distanceBetweenHeads = currentHeadZ - m_LastHeadZ;
        
        if (distanceBetweenHeads > 15 && m_IsFollowing)
        {
            m_IsFollowing = false;
            m_LastHeadZ = currentHeadZ;
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, currentHeadZ - 15f);
            
            StartCoroutine(SmoothMove(targetPos));
        }

        if (_enemyBodyPartHead.transform.position.z < transform.position.z)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
    }

    private IEnumerator SmoothMove(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 2f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, _speed * Time.deltaTime);
            
            yield return null;
        }

        m_IsFollowing = true;
        m_FirstFollow = false;
    }
    
    public bool GetIsFollowing()
    {
        return m_IsFollowing;
    }
    
    public bool GetFirstFollow()
    {
        return m_FirstFollow;
    }
}
