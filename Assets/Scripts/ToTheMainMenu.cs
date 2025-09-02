using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTheMainMenu : MonoBehaviour
{
    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}