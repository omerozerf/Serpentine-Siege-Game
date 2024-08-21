using Cysharp.Threading.Tasks;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _duration;
        
        
    private async void Awake()
    {
        await UniTask.WaitForSeconds(_duration);
    }
}