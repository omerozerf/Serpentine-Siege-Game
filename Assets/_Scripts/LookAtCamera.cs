using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Ana kamerayı bul ve referans olarak sakla
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Kameranın konumuna bakarak, X ekseninde döndürme yap
        Vector3 direction = mainCamera.transform.position - transform.position;
        
        // Y ve Z eksenlerini sıfırla, sadece X eksenini kullan
        direction.y = 0f;
        direction.z = 0f;

        // Yeni yönelimi hesapla
        Quaternion rotation = Quaternion.LookRotation(direction);
        
        // Mevcut rotasyonu güncelle
        transform.rotation = rotation;
    }
}