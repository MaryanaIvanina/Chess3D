using static System.Math;
using UnityEngine;

public class Bishop : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
    private bool isRotate = false;
    private Quaternion startRotation;
    private Vector3 startPosition;
    private Vector3 direction = new Vector3(1, 0, 1);
    private Vector3 direction2 = new Vector3(-1, 0, 1);
    private Vector3 direction3 = new Vector3(1, 0, -1);
    private Vector3 direction4 = new Vector3(-1, 0, -1);
    [SerializeField] private GameManager GameManager;
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
    }

    void Update()
    {
        /*Ray pawnRay1 = new Ray(transform.position, transform.up);
        Debug.DrawRay(pawnRay1.origin, pawnRay1.direction * 10, Color.red);*/
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
                            transform.Rotate(-rotationAmount, 0, 0);
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

                    float bishopMoveUD = ((Abs(movement) / 5f) + 1f) / 2f;
                    float bishopStepUD = bishopMoveUD - bishopMoveUD % 1f;

                    float bishopMoveLR = ((Abs(way) / 5f) + 1f) / 2f;
                    float bishopStepLR = bishopMoveLR - bishopMoveLR % 1f;

                    Collider collider = GetComponent<Collider>();

                    transform.rotation = startRotation;
                    transform.position -= new Vector3(0, moveAmount, 0);
                    isRotate = false;

                    if (bishopStepUD == bishopStepLR)
                    {
                        if (movement > 0 && way > 0)
                        {
                            collider.enabled = false;
                            Ray bishopRay1 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), direction);
                            if (Physics.Raycast(bishopRay1, out RaycastHit hitInfo1))
                            {
                                if (hitInfo1.collider.gameObject.transform.position.z > transform.position.z + bishopStepUD * 10 - 5 && hitInfo1.collider.gameObject.transform.position.x > transform.position.x + bishopStepLR * 10 - 5)
                                {
                                    transform.position += new Vector3(bishopStepLR * 10f, 0, bishopStepUD * 10f);

                                    Ray bishopRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(bishopRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(bishopStepLR * 10f, 0, bishopStepUD * 10f);
                                    }
                                    else
                                    {
                                        if (gameObject.CompareTag("White"))
                                            GameManager.Instance.SetActiveWhite();
                                        else if (gameObject.CompareTag("Black"))
                                            GameManager.Instance.SetActiveBlack();
                                    }
                                }
                            }
                            else
                            {
                                transform.position += new Vector3(bishopStepLR * 10f, 0, bishopStepUD * 10f);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("Black"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement > 0 && way < 0)
                        {
                            collider.enabled = false;
                            Ray bishopRay2 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), direction2);
                            if (Physics.Raycast(bishopRay2, out RaycastHit hitInfo2))
                            {
                                if (hitInfo2.collider.gameObject.transform.position.z > transform.position.z + bishopStepUD * 10 - 5 && hitInfo2.collider.gameObject.transform.position.x < transform.position.x - bishopStepLR * 10 + 5)
                                {
                                    transform.position += new Vector3(-bishopStepLR * 10f, 0, bishopStepUD * 10f);

                                    Ray bishopRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(bishopRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(-bishopStepLR * 10f, 0, bishopStepUD * 10f);
                                    }
                                    else
                                    {
                                        if (gameObject.CompareTag("White"))
                                            GameManager.Instance.SetActiveWhite();
                                        else if (gameObject.CompareTag("Black"))
                                            GameManager.Instance.SetActiveBlack();
                                    }
                                }
                            }
                            else
                            {
                                transform.position += new Vector3(-bishopStepLR * 10f, 0, bishopStepUD * 10f);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("Black"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement < 0 && way > 0)
                        {
                            collider.enabled = false;
                            Ray bishopRay3 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), direction3);
                            if (Physics.Raycast(bishopRay3, out RaycastHit hitInfo3))
                            {
                                if (hitInfo3.collider.gameObject.transform.position.z < transform.position.z - bishopStepUD * 10 + 5 && hitInfo3.collider.gameObject.transform.position.x > transform.position.x + bishopStepLR * 10 - 5)
                                {
                                    transform.position += new Vector3(bishopStepLR * 10f, 0, -bishopStepUD * 10f);

                                    Ray bishopRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(bishopRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(bishopStepLR * 10f, 0, -bishopStepUD * 10f);
                                    }
                                    else
                                    {
                                        if (gameObject.CompareTag("White"))
                                            GameManager.Instance.SetActiveWhite();
                                        else if (gameObject.CompareTag("Black"))
                                            GameManager.Instance.SetActiveBlack();
                                    }
                                }
                            }
                            else
                            {
                                transform.position += new Vector3(bishopStepLR * 10f, 0, -bishopStepUD * 10f);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("Black"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (movement < 0 && way < 0)
                        {
                            collider.enabled = false;
                            Ray bishopRay4 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), direction4);
                            if (Physics.Raycast(bishopRay4, out RaycastHit hitInfo4))
                            {
                                if (hitInfo4.collider.gameObject.transform.position.z < transform.position.z - bishopStepUD * 10 + 5 && hitInfo4.collider.gameObject.transform.position.x < transform.position.x - bishopStepLR * 10 + 5)
                                {
                                    transform.position += new Vector3(-bishopStepLR * 10f, 0, -bishopStepUD * 10f);

                                    Ray bishopRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(bishopRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(-bishopStepLR * 10f, 0, -bishopStepUD * 10f);
                                    }
                                    else
                                    {
                                        if (gameObject.CompareTag("White"))
                                            GameManager.Instance.SetActiveWhite();
                                        else if (gameObject.CompareTag("Black"))
                                            GameManager.Instance.SetActiveBlack();
                                    }
                                }
                            }
                            else
                            {
                                transform.position += new Vector3(-bishopStepLR * 10f, 0, -bishopStepUD * 10f);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("Black"))
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
