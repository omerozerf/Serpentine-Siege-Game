using UnityEngine;

public class EnemyBodyPartLookVisual : MonoBehaviour
{
    [SerializeField] private Transform _lookAtTransform;
    
    private void Update()
    {
        if (_lookAtTransform != null)
        {
            return;
            
            // Hedef pozisyona doğru yönelen rotasyonu hesapla
            Vector3 direction = _lookAtTransform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Sadece Z eksenindeki rotasyonu uygula
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetRotation.eulerAngles.z);
        }
    }
}