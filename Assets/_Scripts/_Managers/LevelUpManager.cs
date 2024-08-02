using Players;
using UnityEngine;
using UnityEngine.UI;

namespace _Managers
{
    public class LevelUpManager : MonoBehaviour
    {
        [SerializeField] private Transform _levelUpParentTransform;
        [SerializeField] private ButtonBuff _levelUpLeftButton;
        [SerializeField] private ButtonBuff _levelUpRightButton;
        
        private static LevelUpManager ms_Instance;


        private void Awake()
        {
            ms_Instance = this;
            PlayerLevelSystem.OnLevelUp += OnLevelUp;
            
            _levelUpLeftButton.GetButton().onClick.AddListener(() =>
            {
                _levelUpParentTransform.gameObject.SetActive(false);
            });
            
            _levelUpRightButton.GetButton().onClick.AddListener(() =>
            {
                _levelUpParentTransform.gameObject.SetActive(false);
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
            
            _levelUpParentTransform.gameObject.SetActive(true);
        }
    }
}