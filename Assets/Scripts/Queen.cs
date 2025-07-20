using static System.Math;
using UnityEngine;

public class Queen : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
    private float queenMoveAmount = 10f;
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

                    float queenMoveUD = ((Abs(movement) / 5f) + 1f) / 2f;
                    float queenStepUD = queenMoveUD - queenMoveUD % 1f;

                    float queenMoveLR = ((Abs(way) / 5f) + 1f) / 2f;
                    float queenStepLR = queenMoveLR - queenMoveLR % 1f;

                    transform.position -= new Vector3(0, moveAmount, 0);
                    transform.rotation = startRotation;
                    isRotate = false;

                    Collider collider = GetComponent<Collider>();

                    if (Abs(way) < 5 && Abs(movement) > 5)
                    {
                        float queenMove = ((Abs(movement) / 5f) + 1f) / 2f;
                        float queenStep = queenMove - queenMove % 1f;
                        float Steps = queenStep * queenMoveAmount;

                        if (movement > 0)
                        {
                            collider.enabled = false;
                            Ray queenRay1 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.forward);
                            if (Physics.Raycast(queenRay1, out RaycastHit hitInfo1))
                            {
                                if (hitInfo1.collider.gameObject.transform.position.z > transform.position.z + Steps - 5)
                                {
                                    transform.position += new Vector3(0, 0, Steps);

                                    Ray queenRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(queenRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(0, 0, Steps);
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
                                transform.position += new Vector3(0, 0, Steps);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("Black"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else
                        {
                            collider.enabled = false;
                            Ray queenRay2 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), -transform.forward);
                            if (Physics.Raycast(queenRay2, out RaycastHit hitInfo2))
                            {
                                if (hitInfo2.collider.gameObject.transform.position.z < transform.position.z - Steps + 5)
                                {
                                    transform.position += new Vector3(0, 0, -Steps);

                                    Ray queenRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(queenRay, out RaycastHit hitInfo))
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
                                            transform.position += new Vector3(0, 0, Steps);
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
                                transform.position += new Vector3(0, 0, -Steps);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("Black"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                    }
                    else if (Abs(movement) < 5 && Abs(way) > 5)
                    {
                        float queenMove = ((Abs(way) / 5f) + 1f) / 2f;
                        float queenStep = queenMove - queenMove % 1f;
                        float Steps = queenStep * queenMoveAmount;

                        if (way > 0)
                        {
                            collider.enabled = false;
                            Ray queenRay3 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.right);
                            if (Physics.Raycast(queenRay3, out RaycastHit hitInfo3))
                            {
                                if (hitInfo3.collider.gameObject.transform.position.x > transform.position.x + Steps - 5)
                                {
                                    transform.position += new Vector3(Steps, 0, 0);

                                    Ray queenRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(queenRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(Steps, 0, 0);
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
                                transform.position += new Vector3(Steps, 0, 0);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("Black"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else
                        {
                            collider.enabled = false;
                            Ray queenRay4 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), -transform.right);
                            if (Physics.Raycast(queenRay4, out RaycastHit hitInfo4))
                            {
                                if (hitInfo4.collider.gameObject.transform.position.x < transform.position.x - Steps + 5)
                                {
                                    transform.position += new Vector3(-Steps, 0, 0);

                                    Ray queenRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(queenRay, out RaycastHit hitInfo))
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
                                            transform.position += new Vector3(Steps, 0, 0);
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
                                transform.position += new Vector3(-Steps, 0, 0);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("Black"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                    }
                    else if (queenStepUD == queenStepLR)
                    {
                        if (movement > 0 && way > 0)
                        {
                            collider.enabled = false;
                            Ray queenRay1 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), direction);
                            if (Physics.Raycast(queenRay1, out RaycastHit hitInfo1))
                            {
                                if (hitInfo1.collider.gameObject.transform.position.z > transform.position.z + queenStepUD * 10 - 5 && hitInfo1.collider.gameObject.transform.position.x > transform.position.x + queenStepLR * 10 - 5)
                                {
                                    transform.position += new Vector3(queenStepLR * 10f, 0, queenStepUD * 10f);

                                    Ray queenRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(queenRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(queenStepLR * 10f, 0, queenStepUD * 10f);
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
                                transform.position += new Vector3(queenStepLR * 10f, 0, queenStepUD * 10f);
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
                            Ray queenRay2 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), direction2);
                            if (Physics.Raycast(queenRay2, out RaycastHit hitInfo2))
                            {
                                if (hitInfo2.collider.gameObject.transform.position.z > transform.position.z + queenStepUD * 10 - 5 && hitInfo2.collider.gameObject.transform.position.x < transform.position.x - queenStepLR * 10 + 5)
                                {
                                    transform.position += new Vector3(-queenStepLR * 10f, 0, queenStepUD * 10f);

                                    Ray queenRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(queenRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(-queenStepLR * 10f, 0, queenStepUD * 10f);
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
                                transform.position += new Vector3(-queenStepLR * 10f, 0, queenStepUD * 10f);
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
                            Ray queenRay3 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), direction3);
                            if (Physics.Raycast(queenRay3, out RaycastHit hitInfo3))
                            {
                                if (hitInfo3.collider.gameObject.transform.position.z < transform.position.z - queenStepUD * 10 + 5 && hitInfo3.collider.gameObject.transform.position.x > transform.position.x + queenStepLR * 10 - 5)
                                {
                                    transform.position += new Vector3(queenStepLR * 10f, 0, -queenStepUD * 10f);

                                    Ray queenRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(queenRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(queenStepLR * 10f, 0, -queenStepUD * 10f);
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
                                transform.position += new Vector3(queenStepLR * 10f, 0, -queenStepUD * 10f);
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
                            Ray queenRay4 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), direction4);
                            if (Physics.Raycast(queenRay4, out RaycastHit hitInfo4))
                            {
                                if (hitInfo4.collider.gameObject.transform.position.z < transform.position.z - queenStepUD * 10 + 5 && hitInfo4.collider.gameObject.transform.position.x < transform.position.x - queenStepLR * 10 + 5)
                                {
                                    transform.position += new Vector3(-queenStepLR * 10f, 0, -queenStepUD * 10f);

                                    Ray queenRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(queenRay, out RaycastHit hitInfo))
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
                                            transform.position -= new Vector3(-queenStepLR * 10f, 0, -queenStepUD * 10f);
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
                                transform.position += new Vector3(-queenStepLR * 10f, 0, -queenStepUD * 10f);
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

