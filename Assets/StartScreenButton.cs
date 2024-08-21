using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class StartScreenButton : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    
    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }
    
    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }
    
    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
}
