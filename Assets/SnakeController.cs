using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _gap;
    [SerializeField] private List<Transform> _bodyPartTransformList;
    
    private readonly List<Vector3> m_PositionHistoryList = new List<Vector3>();


    private void Update()
    {
        transform.Translate(transform.forward * (_speed * Time.deltaTime));
        
        m_PositionHistoryList.Insert(0, transform.position);
        
        int index = 0;
        foreach (Transform bodyPartTransform in _bodyPartTransformList)
        {
            Vector3 point = m_PositionHistoryList[Math.Min(index * _gap, m_PositionHistoryList.Count - 1)];
            bodyPartTransform.position = point;
            index++;
        }
    }
}
