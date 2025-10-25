using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ToTheMainMenu : MonoBehaviour
{
    public void OnMainMenuButtonClicked()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }

        if (GameMode.Instance != null)
        {
            GameMode.Instance.ResetGameMode();
        }
        SceneManager.LoadScene("MainMenu");
    }
}