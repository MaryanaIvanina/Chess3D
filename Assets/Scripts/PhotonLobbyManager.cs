using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PhotonLobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    public TMP_InputField roomNameInput;
    public Button createRoomButton;
    public Button joinRoomButton;
    public Button backButton;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI waitingText;

    [Header("Panels")]
    public GameObject lobbyPanel;
    public GameObject waitingPanel;

    private void Start()
    {
        statusText.text = "Підключення до сервера...";
        lobbyPanel.SetActive(true);
        waitingPanel.SetActive(false);

        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public override void OnConnectedToMaster()
    {
        statusText.text = "✓ Підключено! Введи назву кімнати";
        PhotonNetwork.JoinLobby();

        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        statusText.text = "Помилка підключення: " + cause.ToString();
        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
    }

    public void CreateRoom()
    {
        string roomName = roomNameInput.text.Trim();

        if (string.IsNullOrEmpty(roomName))
        {
            statusText.text = "⚠ Введи назву кімнати!";
            return;
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        options.IsVisible = true;
        options.IsOpen = true;

        PhotonNetwork.CreateRoom(roomName, options);
        statusText.text = "Створюю кімнату '" + roomName + "'...";

        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
    }

    public void JoinRoom()
    {
        string roomName = roomNameInput.text.Trim();

        if (string.IsNullOrEmpty(roomName))
        {
            statusText.text = "⚠ Введи назву кімнати!";
            return;
        }

        PhotonNetwork.JoinRoom(roomName);
        statusText.text = "Приєднуюсь до '" + roomName + "'...";

        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Успішно приєднався до кімнати!");

        lobbyPanel.SetActive(false);
        waitingPanel.SetActive(true);

        UpdateWaitingText();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Новий гравець приєднався: " + newPlayer.NickName);
        UpdateWaitingText();

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            StartGame();
        }
    }


    private void UpdateWaitingText()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        string roomName = PhotonNetwork.CurrentRoom.Name;

        if (playerCount == 1)
        {
            waitingText.text = $"Кімната: {roomName}\n\nОчікування другого гравця...\n(1/2)";
        }
        else
        {
            waitingText.text = $"Кімната: {roomName}\n\nОбидва гравці готові!\nЗапуск гри...";
        }
    }

    private void StartGame()
    {
            PhotonNetwork.LoadLevel("Gameplay");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        statusText.text = "⚠ Помилка: " + message;
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        statusText.text = "⚠ Помилка створення: " + message;
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    public void BackToMainMenu()
    {
        PhotonNetwork.Disconnect();
        GameMode.Instance.gameMode = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Вийшов з кімнати");
    }
}