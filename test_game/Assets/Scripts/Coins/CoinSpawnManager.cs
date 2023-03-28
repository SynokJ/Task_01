using UnityEngine;
using UnityEngine.Events;

public class CoinSpawnManager : MonoBehaviour
{
    [Header("Event Components:")]
    [SerializeField] private UnityEvent _events;

    [Header("Coin Components:")]
    [SerializeField] private GameObject _coinPref;
    [SerializeField] private GameObject _coinParticleEffect;

    [Header("Spawn Components:")]
    [SerializeField]
    private System.Collections.Generic.List<Transform> _spawn
        = new System.Collections.Generic.List<Transform>();

    private SpawnSystem _spawnSystem = null;

    private void Start()
    {
        _spawnSystem = new SpawnSystem(_coinPref, _events, _coinParticleEffect, _spawn);
        _spawnSystem.OnCoinInitialized();
    }

    public void SpawnCoin()
        => _spawnSystem.OnCoinSpawned();
}
