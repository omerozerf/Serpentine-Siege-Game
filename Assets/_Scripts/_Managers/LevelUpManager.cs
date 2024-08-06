using Players;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace _Managers
{
    public class LevelUpManager : MonoBehaviour
    {
        [SerializeField] private Transform _levelUpParentTransform;
        [SerializeField] private ButtonBuff _levelUpLeftButton;
        [SerializeField] private ButtonBuff _levelUpRightButton;
        [SerializeField] private LevelUpPowerUpSO[] _levelUpPowerUpSoArray;
        
        private static LevelUpManager ms_Instance;

        private void Awake()
        {
            ms_Instance = this;
            PlayerLevelSystem.OnLevelUp += OnLevelUp;
            
            _levelUpLeftButton.GetButton().onClick.AddListener(() =>
            {
                _levelUpParentTransform.gameObject.SetActive(false);
                GameManager.SetIsPaused(false);
            });
            
            _levelUpRightButton.GetButton().onClick.AddListener(() =>
            {
                _levelUpParentTransform.gameObject.SetActive(false);
                GameManager.SetIsPaused(false);
            });
        }

        private void OnDestroy()
        {
            PlayerLevelSystem.OnLevelUp -= OnLevelUp;
            
            _levelUpLeftButton.GetButton().onClick.RemoveAllListeners();
            _levelUpRightButton.GetButton().onClick.RemoveAllListeners();
        }

        private void OnLevelUp(int level)
        {
            Debug.Log("Level Up! New Level: " + level);
            GameManager.SetIsPaused(true);
            
            ChangePowerUp();
            
            _levelUpParentTransform.gameObject.SetActive(true);
        }

        public static void ChangePowerUp()
        {
            LevelUpPowerUpSO[] powerUps = ms_Instance._levelUpPowerUpSoArray;

            // Ensure we have at least two different types
            if (powerUps.Length < 2)
            {
                Debug.LogError("Not enough power-ups to select two different types.");
                return;
            }

            LevelUpPowerUpSO leftPowerUp = null;
            LevelUpPowerUpSO rightPowerUp = null;

            while (leftPowerUp == null || rightPowerUp == null ||
                   leftPowerUp.GetPowerUpType() == rightPowerUp.GetPowerUpType())
            {
                leftPowerUp = powerUps[Random.Range(0, powerUps.Length)];
                rightPowerUp = powerUps[Random.Range(0, powerUps.Length)];
            }

            ms_Instance._levelUpLeftButton.SetLevelUpPowerUpSO(leftPowerUp);
            ms_Instance._levelUpRightButton.SetLevelUpPowerUpSO(rightPowerUp);
        }
    }
}
