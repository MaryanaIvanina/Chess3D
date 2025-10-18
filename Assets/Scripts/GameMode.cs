using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    public int gameMode = 0;
    public int botColor = 0;
}
