using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float _ADDITIONAL_TIME_POINTS = 5.0f;

    #region Singleton
    public static GameManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [Header("UI Components:")]
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _winUI;

    [Header("Text Components:")]
    [SerializeField] private TMPro.TextMeshProUGUI _coinAmountText;
    [SerializeField] private TMPro.TextMeshProUGUI _timerText;

    [Header("Game Parameters:")]
    [SerializeField] private int _coinAmountLimit;
    [SerializeField] private float _timerDelay;

    [Header("Player Components:")]
    [SerializeField] private PlayerSpawn _playerSpawn;

    public int CoinAmountLimit { get => _coinAmountLimit; }

    private float _currentTimerDelay = 0;

    private void Start()
    {
        _currentTimerDelay = _timerDelay;
        InvokeRepeating("OnTimerRun", 0, Time.deltaTime);
    }

    private void OnTimerRun()
    {
        if (_currentTimerDelay <= 0)
        {
            _currentTimerDelay = _timerDelay;
            _loseUI.SetActive(true);
            OnGameFinished();
        }
        else
            _currentTimerDelay -= Time.deltaTime;

        _timerText.text = _currentTimerDelay.ToString("0.0");
    }

    public void OnRestartButtonClicked()
    {
        _loseUI.SetActive(false);
        _winUI.SetActive(false);
    }

    public void OnGameWin()
    {
        _winUI.SetActive(true);
        OnGameFinished();
    }

    public void OnGameFinished()
        => _playerSpawn.ResetPlayerPosition();

    public void OnTimeAdded()
    {
        _currentTimerDelay += _ADDITIONAL_TIME_POINTS;
        if (_currentTimerDelay > _timerDelay)
            _currentTimerDelay = _timerDelay;
    }

    public void OnCoinAmountUpdate()
        => _coinAmountText.text = CoinCounter.CoinAmount.ToString();
}
