using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void PawnEnd()
    {
        gameObject.SetActive(true);
    }

    public void PawnStart()
    {
        gameObject.SetActive(false);
    }
}
