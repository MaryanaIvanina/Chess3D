using UnityEngine;
using UnityEngine.Rendering;

public class ChessPieces : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PrefabSpawner _prefabSpawner;
    [SerializeField] private BlackPrefabSpawner _blackPrefabSpawner;

    public void CopyPrefab()
    {
        GameObject newPrefab = Instantiate(prefabToSpawn, _gameManager.position, _gameManager.rotation);
        if (newPrefab.name == "Knight(Clone)")
            newPrefab.transform.Rotate(0, -180f, 0);
        _prefabSpawner.PawnStart();
    }

    public void CopyBlackPrefab()
    {
        GameObject newPrefab = Instantiate(prefabToSpawn, _gameManager.position, _gameManager.rotation);
        if (newPrefab.name == "BlackKnight(Clone)")
            newPrefab.transform.Rotate(0, -180f, 0);
        _blackPrefabSpawner.BlackPawnStart();
    }
}
