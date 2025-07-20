using static System.Math;
using UnityEngine;

public class Rook : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
    private float rookMoveAmount = 10f;
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
        /*Ray pawnRay1 = new Ray(transform.position, transform.up);
        Debug.DrawRay(pawnRay1.origin, pawnRay1.direction * 10, Color.red);*/
        /*Ray pawnRay1 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.forward);
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

                    transform.position -= new Vector3(0, moveAmount, 0);
                    transform.rotation = startRotation;
                    isRotate = false;

                    Collider collider = GetComponent<Collider>();

                    if (Abs(way) < 5 && Abs(movement) > 5)
                    {
                        float rookMove = ((Abs(movement) / 5f) + 1f) / 2f;
                        float rookStep = rookMove - rookMove % 1f;
                        float Steps = rookStep * rookMoveAmount;

                        if (movement > 0)
                        {
                            collider.enabled = false;
                            Ray rookRay1 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.forward);
                            if (Physics.Raycast(rookRay1, out RaycastHit hitInfo1))
                            {
                                if (hitInfo1.collider.gameObject.transform.position.z > transform.position.z + Steps - 5)
                                {
                                    transform.position += new Vector3(0, 0, Steps);

                                    Ray rookRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(rookRay, out RaycastHit hitInfo))
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
                                        else
                                            GameManager.Instance.SetActiveBlack();
                                    }
                                }
                            }
                            else
                            {
                                transform.position += new Vector3(0, 0, Steps);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else
                        {
                            collider.enabled = false;
                            Ray rookRay2 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), -transform.forward);
                            if (Physics.Raycast(rookRay2, out RaycastHit hitInfo2))
                            {
                                if (hitInfo2.collider.gameObject.transform.position.z < transform.position.z - Steps + 5)
                                {
                                    transform.position += new Vector3(0, 0, -Steps);

                                    Ray rookRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(rookRay, out RaycastHit hitInfo))
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
                                        else
                                            GameManager.Instance.SetActiveBlack();
                                    }
                                }
                            }
                            else
                            {
                                transform.position += new Vector3(0, 0, -Steps);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                    }
                    else if (Abs(movement) < 5 && Abs(way) > 5)
                    {
                        float rookMove = ((Abs(way) / 5f) + 1f) / 2f;
                        float rookStep = rookMove - rookMove % 1f;
                        float Steps = rookStep * rookMoveAmount;

                        if (way > 0)
                        {
                            collider.enabled = false;
                            Ray rookRay3 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.right);
                            if (Physics.Raycast(rookRay3, out RaycastHit hitInfo3))
                            {
                                if (hitInfo3.collider.gameObject.transform.position.x > transform.position.x + Steps - 5)
                                {
                                    transform.position += new Vector3(Steps, 0, 0);

                                    Ray rookRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(rookRay, out RaycastHit hitInfo))
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
                                        else
                                            GameManager.Instance.SetActiveBlack();
                                    }
                                }
                            }
                            else
                            {
                                transform.position += new Vector3(Steps, 0, 0);
                                if (gameObject.CompareTag("White"))
                                    GameManager.Instance.SetActiveWhite();
                                else
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else
                        {
                            collider.enabled = false;
                            Ray rookRay4 = new Ray(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), -transform.right);
                            if (Physics.Raycast(rookRay4, out RaycastHit hitInfo4))
                            {
                                if (hitInfo4.collider.gameObject.transform.position.x < transform.position.x - Steps + 5)
                                {
                                    transform.position += new Vector3(-Steps, 0, 0);

                                    Ray rookRay = new Ray(transform.position, transform.up);
                                    if (Physics.Raycast(rookRay, out RaycastHit hitInfo))
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
                                        else
                                            GameManager.Instance.SetActiveBlack();
                                    }
                                }
                            }
                            else
                            {
                                transform.position += new Vector3(-Steps, 0, 0);
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
