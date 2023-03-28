using UnityEngine;
using UnityEngine.Events;

public class CoinController : MonoBehaviour
{
    private UnityEvent _events = default;
    private PlayerController _playerController = default;

    public void EventInitialized(UnityEvent events)
        => _events = events;

    private void OnTriggerEnter(Collider other)
    {
        _playerController = other.GetComponent<PlayerController>();
        if (_playerController != null)
        {
            CoinCounter.AddCoin();
            _events?.Invoke();
        }
    }
}
