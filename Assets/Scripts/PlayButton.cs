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
        _gameMode.botColor = Random.Range(1, 3);
    }
    public void OnThisDeviceButtonClicked()
    {
        _gameMode.gameMode = 2;
    }
    public void OnMultiplayerButtonClicked()
    {
        _gameMode.gameMode = 3;
        SceneManager.LoadScene("LobbyScene");
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
