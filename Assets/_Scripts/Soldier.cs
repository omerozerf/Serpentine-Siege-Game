using Players;
using UnityEngine;
using UnityEngine.Serialization;

public class Soldier : MonoBehaviour
{
    [SerializeField] private BulletShooter _bulletShooter;
    
    
    public BulletShooter GetBulletShooter()
    {
        return _bulletShooter;
    }
}