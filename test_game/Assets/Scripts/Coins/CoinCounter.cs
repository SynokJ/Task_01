public static class CoinCounter
{
    public static int CoinAmount { get => _coinAmount; }
    private static int _coinAmount = 0;

    private static GameManager _gameManager = null;

    public static void AddCoin()
    {
        if (_gameManager == null)
            _gameManager = GameManager.Instance;

        _coinAmount++;

        if (_coinAmount >= _gameManager.CoinAmountLimit)
        {
            _gameManager.OnGameWin();
            _coinAmount = 0;
        }
    }
}
