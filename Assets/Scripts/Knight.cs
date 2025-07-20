using static System.Math;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
    private float knightMoveAmount1 = 20f;
    private float knightMoveAmount2 = 10f;
    private bool isRotate = false;
    private Quaternion startRotation;
    private Vector3 startPosition;
    [SerializeField] private GameManager GameManager;
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (!isRotate)
                    {
                        if (gameObject.CompareTag("Black"))
                            transform.Rotate(rotationAmount, 0, 0);
                        else transform.Rotate(rotationAmount, 0, 0);
                        transform.position += new Vector3(0, moveAmount, 0);
                        isRotate = true;
                    }
                    else
                    {
                        transform.rotation = startRotation;
                        transform.position -= new Vector3(0, moveAmount, 0);
                        isRotate = false;
                    }
                }
                else if (!isRotate)
                {
                    return;
                }
                else
                {
                    Vector3 newPosition = hit.point;
                    float movement = newPosition.z - transform.position.z;
                    float way = newPosition.x - transform.position.x;

                    transform.position -= new Vector3(0, moveAmount, 0);
                    transform.rotation = startRotation;
                    isRotate = false;

                    Collider collider = GetComponent<Collider>();

                    if (Abs(way) < 15 && Abs(way) > 5 && Abs(movement) < 25 && Abs(movement) > 15)
                    {
                        if (movement > 0 && way > 0)
                        {
                            transform.position += new Vector3(knightMoveAmount2, 0, knightMoveAmount1);

                            collider.enabled = false;
                            Ray knightRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(knightRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("White") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(knightMoveAmount2, 0, knightMoveAmount1);
                            }
                            else
                            {
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement > 0 && way < 0)
                        {
                            transform.position += new Vector3(-knightMoveAmount2, 0, knightMoveAmount1);

                            collider.enabled = false;
                            Ray knightRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(knightRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("White") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(-knightMoveAmount2, 0, knightMoveAmount1);
                            }
                            else
                            {
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement < 0 && way > 0)
                        {
                            transform.position += new Vector3(knightMoveAmount2, 0, -knightMoveAmount1);

                            collider.enabled = false;
                            Ray knightRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(knightRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("White") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(knightMoveAmount2, 0, -knightMoveAmount1);
                            }
                            else
                            {
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement < 0 && way < 0)
                        {
                            transform.position += new Vector3(-knightMoveAmount2, 0, -knightMoveAmount1);

                            collider.enabled = false;
                            Ray knightRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(knightRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("White") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(-knightMoveAmount2, 0, -knightMoveAmount1);
                            }
                            else
                            {
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                    }
                    else if (Abs(way) > 15 && Abs(way) < 25 && Abs(movement) > 5 && Abs(movement) < 15)
                    {
                        if (movement > 0 && way > 0)
                        {
                            transform.position += new Vector3(knightMoveAmount1, 0, knightMoveAmount2);

                            collider.enabled = false;
                            Ray knightRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(knightRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("White") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(knightMoveAmount1, 0, knightMoveAmount2);
                            }
                            else
                            {
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement > 0 && way < 0)
                        {
                            transform.position += new Vector3(-knightMoveAmount1, 0, knightMoveAmount2);

                            collider.enabled = false;
                            Ray knightRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(knightRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("White") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(-knightMoveAmount1, 0, knightMoveAmount2);
                            }
                            else
                            {
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement < 0 && way > 0)
                        {
                            transform.position += new Vector3(knightMoveAmount1, 0, -knightMoveAmount2);

                            collider.enabled = false;
                            Ray knightRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(knightRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("White") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                { 
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(knightMoveAmount1, 0, -knightMoveAmount2);
                            }
                            else
                            {
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement < 0 && way < 0)
                        {
                            transform.position += new Vector3(-knightMoveAmount1, 0, -knightMoveAmount2);

                            collider.enabled = false;
                            Ray knightRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(knightRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("White") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(-knightMoveAmount1, 0, -knightMoveAmount2);
                            }
                            else
                            {
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                    }
                }
            }
        }
    }
}
