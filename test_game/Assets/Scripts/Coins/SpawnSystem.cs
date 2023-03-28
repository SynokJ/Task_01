public class SpawnSystem
{
    private System.Collections.Generic.List<UnityEngine.Transform> _spawnPos
        = new System.Collections.Generic.List<UnityEngine.Transform>();

    private System.Collections.Generic.List<UnityEngine.Vector3> _awailableSpawnPos
        = new System.Collections.Generic.List<UnityEngine.Vector3>();

    private UnityEngine.GameObject _coinPref = default;
    private UnityEngine.GameObject _currentCoin = default;
    private UnityEngine.GameObject _coinParticlePref = default;

    private UnityEngine.Vector3 _lastSpawnPos = default;
    private UnityEngine.Vector3 _particlePos = default;

    private UnityEngine.Events.UnityEvent _events = default;

    public SpawnSystem(UnityEngine.GameObject coinPref, UnityEngine.Events.UnityEvent events, UnityEngine.GameObject particlePref, System.Collections.Generic.List<UnityEngine.Transform> spawnPos)
    {
        _events = events;
        _coinPref = coinPref;
        _coinParticlePref = particlePref;
        _spawnPos = new System.Collections.Generic.List<UnityEngine.Transform>(spawnPos);

        InitAwailablePosition();
    }

    ~SpawnSystem()
    {
        _coinPref = null;
        _currentCoin = null;
        _coinParticlePref = null;

        _spawnPos.Clear();
        _awailableSpawnPos.Clear();
    }

    public void OnCoinInitialized()
    {
        int rndPosId = UnityEngine.Random.Range(0, _spawnPos.Count);
        _currentCoin =
            UnityEngine.GameObject.Instantiate(_coinPref, _spawnPos[rndPosId].position, UnityEngine.Quaternion.identity);

        CoinController coinController = _currentCoin.GetComponent<CoinController>();
        if (coinController != null)
            coinController.EventInitialized(_events);

        _lastSpawnPos = _spawnPos[rndPosId].position;
        _awailableSpawnPos.Remove(_lastSpawnPos);
    }

    public void OnCoinSpawned()
    {
        _currentCoin.transform.position = GetRandomSpawnPosition();
        UnityEngine.GameObject.Instantiate(_coinParticlePref, _particlePos, UnityEngine.Quaternion.identity);
    }

    public UnityEngine.Vector3 GetRandomSpawnPosition()
    {
        _particlePos = _lastSpawnPos;

        int rndPosId = UnityEngine.Random.Range(0, _awailableSpawnPos.Count);
        UnityEngine.Vector3 res = _awailableSpawnPos[rndPosId];
        _awailableSpawnPos.Add(_lastSpawnPos);

        _lastSpawnPos = _awailableSpawnPos[rndPosId];
        _awailableSpawnPos.Remove(_lastSpawnPos);

        return res;
    }

    private void InitAwailablePosition()
    {
        for (int i = 0; i < _spawnPos.Count; ++i)
            _awailableSpawnPos.Add(_spawnPos[i].position);
    }
}
