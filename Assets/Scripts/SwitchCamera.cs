using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public int cameraIndex = 0;
    private int cameraButtonIndex = 0;

    private void Start()
    {
        SwitchCameraPosition();
    }

    public void SwitchCameraPosition()
    {
        if (cameraIndex == 0)
        {
            transform.position = new Vector3(40, 96, 40);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else if (cameraIndex == 1)
        {
            transform.position = new Vector3(40, 96, 40);
            transform.rotation = Quaternion.Euler(90, 0, 180);
        }
    }

    public void OnCameraClick()
    {
        if (cameraButtonIndex == 0)
        {
            transform.position = new Vector3(126, 65, -18);
            transform.rotation = Quaternion.Euler(32, -416, 0);
            cameraButtonIndex = 1;
        }
        else
        {
            SwitchCameraPosition();
            cameraButtonIndex = 0;
        }
    }
}
