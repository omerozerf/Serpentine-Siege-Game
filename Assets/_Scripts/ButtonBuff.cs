using System;
using System.Collections;
using System.Collections.Generic;
using _Managers;
using Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBuff : MonoBehaviour
{
    [SerializeField] private LevelUpPowerUpSO _levelUpPowerUpSO;
    [SerializeField] private Button _button;
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _header;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _image;
    
    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            switch (_levelUpPowerUpSO._powerUpType)
            {
                case LevelUpPowerUpType.FireRate:
                {
                    _player.GetPlayerBulletShooter().AddFireRate(_levelUpPowerUpSO.GetFireRatePercentage());
                    break;
                }
                case LevelUpPowerUpType.Damage:
                {
                    GameManager.SetBulletDamage(_levelUpPowerUpSO.GetDamageAmount());
                    break;
                }
                case LevelUpPowerUpType.MovementSpeed:
                {
                    _player.GetPlayerMover().AddMoveSpeed(_levelUpPowerUpSO.GetMovementSpeedPercentage());
                    break;
                }
                case LevelUpPowerUpType.EnemySpeed:
                {
                    GameManager.AddEnemyMoveSpeed(_levelUpPowerUpSO.GetEnemySpeedPercentage());
                    break;
                }
                case LevelUpPowerUpType.SoldierCount:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }

    private void OnEnable()
    {
        _header.text = _levelUpPowerUpSO.GetPowerUpHeader();
        _description.text = _levelUpPowerUpSO.GetPowerUpDescription();
        string icon = _levelUpPowerUpSO.GetPowerUpValue() > 0 ? "+" : "-";
        _description.text = $"{icon} %{Mathf.Abs(_levelUpPowerUpSO.GetPowerUpValue())}";
        _image.sprite = _levelUpPowerUpSO.GetPowerUpSprite();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
    
    public Button GetButton()
    {
        return _button;
    }
}
