using static System.Math;
using UnityEngine;
using System.Collections.Generic;

public class King : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
    private float kingMoveAmount = 10f;
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
                        if (gameObject.CompareTag("BlackKing"))
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

                    float kingMoveUD = ((Abs(movement) / 5f) + 1f) / 2f;
                    float kingStepUD = kingMoveUD - kingMoveUD % 1f;

                    float kingMoveLR = ((Abs(way) / 5f) + 1f) / 2f;
                    float kingStepLR = kingMoveLR - kingMoveLR % 1f;

                    transform.position -= new Vector3(0, moveAmount, 0);
                    transform.rotation = startRotation;
                    isRotate = false;

                    Collider collider = GetComponent<Collider>();

                    if (Abs(way) < 5 && Abs(movement) > 5 && Abs(movement) < 15)
                    {
                        if (movement > 0)
                        {
                            transform.position += new Vector3(0, 0, kingMoveAmount);

                            collider.enabled = false;
                            Ray kingRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(kingRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("King") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("BlackKing") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(0, 0, kingMoveAmount);
                            }
                            else 
                            {
                                if (gameObject.CompareTag("King"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("BlackKing"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else
                        {
                            transform.position += new Vector3(0, 0, -kingMoveAmount);

                            collider.enabled = false;
                            Ray kingRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(kingRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("King") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("BlackKing") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(0, 0, -kingMoveAmount);
                            }
                            else 
                            {
                                if (gameObject.CompareTag("King"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("BlackKing"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                    }
                    else if (Abs(movement) < 5 && Abs(way) > 5 && Abs(way) < 15)
                    {
                        if (way > 0)
                        {
                            transform.position += new Vector3(kingMoveAmount, 0, 0);

                            collider.enabled = false;
                            Ray kingRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(kingRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("King") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("BlackKing") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(kingMoveAmount, 0, 0);
                            }
                            else 
                            {
                                if (gameObject.CompareTag("King"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("BlackKing"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else
                        {
                            transform.position += new Vector3(-kingMoveAmount, 0, 0);

                            collider.enabled = false;
                            Ray kingRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(kingRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("King") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("BlackKing") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(-kingMoveAmount, 0, 0);
                            }
                            else 
                            {
                                if (gameObject.CompareTag("King"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("BlackKing"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                    }
                    else if (kingStepUD == kingStepLR && kingStepUD == 1f)
                    {
                        if (way > 0 && movement > 0)
                        {
                            transform.position += new Vector3(kingMoveAmount, 0, kingMoveAmount);

                            collider.enabled = false;
                            Ray kingRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(kingRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("King") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("BlackKing") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(kingMoveAmount, 0, kingMoveAmount);
                            }
                            else 
                            {
                                if (gameObject.CompareTag("King"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("BlackKing"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (way < 0 && movement > 0)
                        {
                            transform.position += new Vector3(-kingMoveAmount, 0, kingMoveAmount);

                            collider.enabled = false;
                            Ray kingRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(kingRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("King") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("BlackKing") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(-kingMoveAmount, 0, kingMoveAmount);
                            }
                            else 
                            {
                                if (gameObject.CompareTag("King"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("BlackKing"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else if (way > 0 && movement < 0)
                        {
                            transform.position += new Vector3(kingMoveAmount, 0, -kingMoveAmount);

                            collider.enabled = false;
                            Ray kingRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(kingRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("King") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("BlackKing") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(kingMoveAmount, 0, -kingMoveAmount);
                            }
                            else 
                            {
                                if (gameObject.CompareTag("King"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("BlackKing"))
                                    GameManager.Instance.SetActiveBlack();
                            }
                            collider.enabled = true;
                        }
                        else
                        {
                            transform.position += new Vector3(-kingMoveAmount, 0, -kingMoveAmount);

                            collider.enabled = false;
                            Ray kingRay = new Ray(transform.position, transform.up);
                            if (Physics.Raycast(kingRay, out RaycastHit hitInfo))
                            {
                                if (gameObject.CompareTag("King") && hitInfo.collider.gameObject.CompareTag("Black"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveWhite();
                                }
                                else if (gameObject.CompareTag("BlackKing") && hitInfo.collider.gameObject.CompareTag("White"))
                                {
                                    hitInfo.collider.gameObject.SetActive(false);
                                    GameManager.Instance.SetActiveBlack();
                                }
                                else
                                    transform.position -= new Vector3(-kingMoveAmount, 0, -kingMoveAmount);
                            }
                            else 
                            {
                                if (gameObject.CompareTag("King"))
                                    GameManager.Instance.SetActiveWhite();
                                else if (gameObject.CompareTag("BlackKing"))
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