using UnityEngine;

public class RulesButton : MonoBehaviour
{
    private int clickCount = 0;

    void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnRulesClick()
    {
        if (clickCount == 1)
        {
            gameObject.SetActive(false);
            clickCount = 0;
        }
        else
        {
            gameObject.SetActive(true);
            clickCount = 1;
        }

    }
}