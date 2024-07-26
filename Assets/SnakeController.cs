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

        // Steer
        float steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1
        transform.Rotate(Vector3.up * (steerDirection * _steerSpeed * Time.deltaTime));

        // Store position history
        m_PositionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (GameObject body in m_BodyParts)
        {
            if (!body) continue;
            
            Vector3 point = m_PositionsHistory[Mathf.Clamp(index * _gap, 0, m_PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * (_bodySpeed * Time.deltaTime);

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }
    }

    private void GrowSnake() 
    {
        // Instantiate body instance and
        // add it to the list
        GameObject body = Instantiate(_bodyPrefab);
        m_BodyParts.Add(body);
    }
}