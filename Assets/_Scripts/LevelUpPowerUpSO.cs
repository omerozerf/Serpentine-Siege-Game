using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpPowerUpSO", menuName = "LevelUpPowerUpSO", order = 0)]
public class LevelUpPowerUpSO : ScriptableObject
{
    [SerializeField] public string _powerUpHeader;
    [SerializeField, HideInInspector] public string _powerUpDescription;
    [SerializeField] public Sprite _powerUpSprite;
    [SerializeField] public LevelUpPowerUpType _powerUpType;

    [SerializeField] public float _fireRatePercentage;
    [SerializeField] public int _damageAmount;
    [SerializeField] public float _movementSpeedPercentage;
    [SerializeField] public float _enemySpeedPercentage;
    [SerializeField] public int _soldierCount;

    public string GetPowerUpHeader()
    {
        return _powerUpHeader;
    }

    public string GetPowerUpDescription()
    {
        return _powerUpDescription;
    }

    public Sprite GetPowerUpSprite()
    {
        return _powerUpSprite;
    }

    public LevelUpPowerUpType GetPowerUpType()
    {
        return _powerUpType;
    }

    public float GetFireRatePercentage()
    {
        return _powerUpType == LevelUpPowerUpType.FireRate ? _fireRatePercentage : 0;
    }

    public int GetDamageAmount()
    {
        return _powerUpType == LevelUpPowerUpType.Damage ? _damageAmount : 0;
    }

    public float GetMovementSpeedPercentage()
    {
        return _powerUpType == LevelUpPowerUpType.MovementSpeed ? _movementSpeedPercentage : 0;
    }

    public float GetEnemySpeedPercentage()
    {
        return _powerUpType == LevelUpPowerUpType.EnemySpeed ? _enemySpeedPercentage : 0;
    }

    public int GetSoldierCount()
    {
        return _powerUpType == LevelUpPowerUpType.SoldierCount ? _soldierCount : 0;
    }
    
    public float GetPowerUpValue()
    {
        return _powerUpType switch
        {
            LevelUpPowerUpType.FireRate => _fireRatePercentage,
            LevelUpPowerUpType.Damage => _damageAmount,
            LevelUpPowerUpType.MovementSpeed => _movementSpeedPercentage,
            LevelUpPowerUpType.EnemySpeed => _enemySpeedPercentage,
            LevelUpPowerUpType.SoldierCount => _soldierCount,
            _ => 0
        };
    }
}