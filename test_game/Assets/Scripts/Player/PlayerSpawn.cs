using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Header("Player Components:")]
    [SerializeField] private GameObject _playerPref;

    private GameObject _player = default;
    private PlayerController _playerController = default;

    private void Start()
    {
        _player = Instantiate(_playerPref);
        _playerController = _player.GetComponent<PlayerController>();
    }

    public void ResetPlayerPosition()
    {
        _player.transform.position = Vector3.zero;
        _playerController.OnNavigationReseted();
    }
}
