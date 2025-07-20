using UnityEngine;

public class BlackPrefabSpawner : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void BlackPawnEnd()
    {
        gameObject.SetActive(true);
    }

    public void BlackPawnStart()
    {
        gameObject.SetActive(false);
    }
}
