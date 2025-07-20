using static System.Math;
using UnityEngine;

public class BlackPawn : MonoBehaviour
{
    private float rotationAmount = -20f;
    private float moveAmount = 1f;
    private float pawnMoveAmount = -10f;
    private bool isRotate = false;
    private int stepCount;
    private Quaternion startRotation;
    private Vector3 startPosition;
    [SerializeField] private GameManager GameManager;
    [SerializeField] private BlackPrefabSpawner blackPrefabSpawner;

    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        stepCount = 0;
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
                        transform.Rotate(rotationAmount, 0, 0);
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

                    float pawnMoveUD = ((Abs(movement) / 5f) + 1f) / 2f;
                    float pawnStepUD = pawnMoveUD - pawnMoveUD % 1f;

                    float pawnMoveLR = ((Abs(way) / 5f) + 1f) / 2f;
                    float pawnStepLR = pawnMoveLR - pawnMoveLR % 1f;

                    transform.position -= new Vector3(0, moveAmount, 0);
                    transform.rotation = startRotation;
                    isRotate = false;

                    Collider collider = GetComponent<Collider>();

                    if (pawnStepUD == pawnStepLR && pawnStepUD == 1)
                    {
                        if (way > 0 && movement < 0)
                        {
                            transform.position += new Vector3(-pawnMoveAmount, 0, pawnMoveAmount);

                            collider.enabled = false;
                            Ray pawnRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(pawnRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    stepCount++;
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(-pawnMoveAmount, 0, pawnMoveAmount);
                            }
                            else
                                transform.position -= new Vector3(-pawnMoveAmount, 0, pawnMoveAmount);
                            collider.enabled = true;
                        }
                        else if (way < 0 && movement < 0)
                        {
                            transform.position += new Vector3(pawnMoveAmount, 0, pawnMoveAmount);

                            collider.enabled = false;
                            Ray pawnRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(pawnRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("Black") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    stepCount++;
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(pawnMoveAmount, 0, pawnMoveAmount);
                            }
                            else
                                transform.position -= new Vector3(pawnMoveAmount, 0, pawnMoveAmount);
                            collider.enabled = true;
                        }
                    }
                    else if (movement < 5f && movement > 2f * (pawnMoveAmount - 5f) && Abs(way) < 5)
                    {
                        if (movement > pawnMoveAmount - 5f)
                        {
                            transform.position += new Vector3(0, 0, pawnMoveAmount);

                            collider.enabled = false;
                            Ray pawnRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(pawnRay, out RaycastHit hitInfo))
                                transform.position -= new Vector3(0, 0, pawnMoveAmount);
                            else
                            {
                                stepCount++;
                                GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else
                        {
                            if (stepCount == 0 && movement < 2f * pawnMoveAmount + 5f)
                            {
                                transform.position += new Vector3(0, 0, 2f * pawnMoveAmount);

                                collider.enabled = false;
                                Ray pawnRay = new Ray(transform.position, transform.up);
                                if (Physics.Raycast(pawnRay, out RaycastHit hitInfo))
                                    transform.position -= new Vector3(0, 0, 2f * pawnMoveAmount);
                                else
                                {
                                    stepCount++;
                                    GameManager.Instance.SetActiveBlack();
                                }
                                collider.enabled = true;
                            }
                        }
                    }
                }
            }
        }
        if (transform.position.z == 5)
        {
            GameManager.position = transform.position;
            GameManager.rotation = transform.rotation;
            blackPrefabSpawner.BlackPawnEnd();
            gameObject.SetActive(false);
            GameManager.Instance.SetActiveBlack();
        }
    }
}
