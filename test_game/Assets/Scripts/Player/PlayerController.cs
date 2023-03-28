using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Touch Parameters:")]
    [SerializeField] private LayerMask _hitLayerMask;

    [Header("Movement Components:")]
    [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    [SerializeField] private CoinSpawnManager _coinSpawnManager;

    private InputSystem _inputSystem = null;

    private void Start()
    {
        _inputSystem = new InputSystem(_hitLayerMask, OnDestiantionSet);
    }

    private void Update()
    {
        _inputSystem?.OnScreenTouched();
    }

    private void OnDestiantionSet(Vector3 destiantionPosition)
        => _navMeshAgent.destination = destiantionPosition;

    public void OnNavigationReseted()
        => _navMeshAgent.destination = transform.position;
}
