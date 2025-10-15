using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private GameMode _gameMode;
    void Start()
    {
        _gameMode = GameMode.Instance;
    }
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void OnBotModeButtonClicked()
    {
        _gameMode.gameMode = 1;
    }
    public void OnThisDeviceButtonClicked()
    {
        _gameMode.gameMode = 2;
    }
    public void OnMultiplayerButtonClicked()
    {
        _gameMode.gameMode = 3;
    }
}
