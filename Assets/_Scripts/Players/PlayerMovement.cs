using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchCurrentPos;
    private bool isTouching = false;

    private void Update()
    {
        // Dokunma algılama
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Dokunma başladığında pozisyonu kaydet
                touchStartPos = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                // Dokunma hareket ettiğinde veya sabit durduğunda pozisyonu güncelle
                touchCurrentPos = touch.position;

                // Karakteri parmağın x ekseninde hareket ettir
                MoveCharacterToTouchPosition();
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
    }

    private void MoveCharacterToTouchPosition()
    {
        // Parmağın x pozisyonunu dünya koordinatlarına çevir
        float touchXWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchCurrentPos.x, touchCurrentPos.y, Camera.main.nearClipPlane)).x;
        
        // Karakterin x pozisyonunu parmağın x pozisyonuna ayarla
        transform.position = new Vector3(touchXWorldPos, transform.position.y, transform.position.z);
    }
}