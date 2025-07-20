using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetActiveBlack();
    }

    public void SetActiveWhite()
    {
        SetActivePlayers[] allPieces = FindObjectsByType<SetActivePlayers>(FindObjectsSortMode.None);

        foreach (SetActivePlayers piece in allPieces)
        {
            piece.SetActiveWhite();
        }
    }

    public void SetActiveBlack()
    {
        SetActivePlayers[] allPieces = FindObjectsByType<SetActivePlayers>(FindObjectsSortMode.None);

        foreach (SetActivePlayers piece in allPieces)
        {
            piece.SetActiveBlack();
        }
    }
}