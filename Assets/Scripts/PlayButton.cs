using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class PlayButton : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private Button botModeButton;
    [SerializeField] private Button thisDeviceButton;
    [SerializeField] private Button multiplayerButton;
    [SerializeField] private Button quitButton;

    private GameMode _gameMode;

    void Start()
    {
        _gameMode = GameMode.Instance;
        SetupButtons();
    }

    private void SetupButtons()
    {
        if (botModeButton != null)
        {
            botModeButton.onClick.RemoveAllListeners();
            botModeButton.onClick.AddListener(OnBotModeButtonClicked);
        }

        if (thisDeviceButton != null)
        {
            thisDeviceButton.onClick.RemoveAllListeners();
            thisDeviceButton.onClick.AddListener(OnThisDeviceButtonClicked);
        }

        if (multiplayerButton != null)
        {
            multiplayerButton.onClick.RemoveAllListeners();
            multiplayerButton.onClick.AddListener(OnMultiplayerButtonClicked);
        }

        if (quitButton != null)
        {
            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
    }

    public void OnBotModeButtonClicked()
    {
        _gameMode.gameMode = 1;
        _gameMode.botColor = Random.Range(1, 3);
        SceneManager.LoadScene("Gameplay");
    }

    public void OnThisDeviceButtonClicked()
    {
        _gameMode.gameMode = 2;
        SceneManager.LoadScene("Gameplay");
    }

    public void OnMultiplayerButtonClicked()
    {
        _gameMode.gameMode = 3;
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnQuitButtonClicked()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }

        Application.Quit();
    }
}