using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SnakeController : MonoBehaviour {

    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _steerSpeed = 180;
    [SerializeField] private float _bodySpeed = 5;
    [SerializeField] private int _gap = 10;
    [SerializeField] private GameObject _bodyPrefab;
    
    private readonly List<GameObject> m_BodyParts = new List<GameObject>();
    private readonly List<Vector3> m_PositionsHistory = new List<Vector3>();

    private void Start() 
    {
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();
        GrowSnake();

    }

    private void Update() 
    {
        transform.position += transform.forward * (_moveSpeed * Time.deltaTime);

        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * (steerDirection * _steerSpeed * Time.deltaTime));

        m_PositionsHistory.Insert(0, transform.position);

        int index = 0;
        foreach (GameObject body in m_BodyParts)
        {
            if (!body) continue;
            
            Vector3 point = m_PositionsHistory[Mathf.Clamp(index * _gap, 0, m_PositionsHistory.Count - 1)];

            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * (_bodySpeed * Time.deltaTime);

            body.transform.LookAt(point);

            index++;
        }
    }

    private void GrowSnake() 
    {
        GameObject body = Instantiate(_bodyPrefab);
        m_BodyParts.Add(body);
    }
}