using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       
    public float turnSpeed = 100f;     
    public float turnInterval = 1f;    

    private float nextTurnTime = 0f;   
    private Quaternion targetRotation;
    private int flag;

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
        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
    }

    void HandleTurning()
    {
        if (Time.time >= nextTurnTime)
        {
            SetRandomRotation();
            nextTurnTime = Time.time + turnInterval;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void SetRandomRotation()
    {
        // Y ekseni etrafında -90 ile 90 derece arasında rastgele bir açı belirle
        float randomYRotation;
        if (flag % 2 == 0)
        { 
            randomYRotation = 270;
        }
        else
        {
            randomYRotation = 90;
        }
        flag++;
        
        targetRotation = Quaternion.Euler(0, randomYRotation, 0);
    }
}