using UnityEngine;
using System.Collections.Generic;

public enum PieceColor { White, Black }
public enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King }

public abstract class ChessPiece : MonoBehaviour
{
    [Header("Piece Settings")]
    public PieceColor pieceColor;
    public PieceType pieceType;

    [Header("Animation Settings")]
    [SerializeField] protected float rotationAmount = 10f;
    [SerializeField] protected float moveUpAmount = 1f;

    protected bool isSelected = false;
    protected Quaternion startRotation;
    protected Vector3 startPosition;
    protected ChessGameManager gameManager;

    protected virtual void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        gameManager = ChessGameManager.Instance;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMyTurn())
            HandleMouseClick();
    }

    private void HandleMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform) ToggleSelection();
            else if (isSelected)
            {
                DeselectPiece();
                TryMoveTo(hit.point);
            }
        }
    }

    private void ToggleSelection()
    {
        if (!isSelected) SelectPiece();
        else DeselectPiece();
    }

    private void SelectPiece()
    {
        isSelected = true;
        if (pieceColor == PieceColor.Black)
            transform.Rotate(-rotationAmount, 0, 0);
        else
            transform.Rotate(rotationAmount, 0, 0);

        transform.position += new Vector3(0, moveUpAmount, 0);
    }

    private void DeselectPiece()
    {
        isSelected = false;
        transform.rotation = startRotation;
        transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
    }

    protected virtual void TryMoveTo(Vector3 targetWorldPos)
    {
        Vector3 targetBoardPos = WorldToBoardPosition(targetWorldPos);

        if (IsValidMove(transform.position, targetBoardPos, pieceColor) &&
            !gameManager.WouldKingBeInCheck(pieceColor, this, targetBoardPos))
        {
            ExecuteMove(targetBoardPos);
        }
    }

    public virtual bool IsValidMove(Vector3 from, Vector3 to, PieceColor currentColor)
    {
        return !IsPathBlocked(from, to) && IsLegalMovePattern(from, to) && !IsPositionOccupied(currentColor, to);
    }

    protected abstract bool IsLegalMovePattern(Vector3 from, Vector3 to);

    protected virtual bool IsPathBlocked(Vector3 from, Vector3 to)
    {
        if (pieceType == PieceType.Knight) return false;

        float distance = Vector3.Distance(from, to);
        Vector3 direction = (to - from).normalized;
        Ray ray = new Ray(from + Vector3.up * 5, direction);
        Debug.DrawRay(ray.origin, ray.direction * (distance - 5f), Color.red, 2);

        if (Physics.Raycast(ray, out RaycastHit hit, distance - 5f))
        {
            Debug.DrawRay(ray.origin, ray.direction * (distance - 5f), Color.green, 2);
            return true;
        }
        return false;
    }

    protected virtual bool IsPositionOccupied(PieceColor currentColor, Vector3 targetPos)
    {
        ChessPiece occupyingPiece = GetPieceAtPosition(targetPos);
        return occupyingPiece != null && occupyingPiece.pieceColor == currentColor;
    }

    protected virtual void ExecuteMove(Vector3 targetPos)
    {
        ChessPiece capturedPiece = GetPieceAtPosition(targetPos);
        Move(targetPos, capturedPiece);
    }

    public ChessPiece GetPieceAtPosition(Vector3 position)
    {
        ChessPiece[] allPieces = FindObjectsByType<ChessPiece>(FindObjectsSortMode.None);
        foreach (ChessPiece piece in allPieces)
        {
            if (Vector3.Distance(piece.transform.position, position) < 5f &&
                piece.gameObject.activeInHierarchy)
            {
                return piece;
            }
        }
        return null;
    }

    protected bool IsMyTurn()
    {
        return gameManager.currentTurn == pieceColor && enabled;
    }

    protected Vector3 WorldToBoardPosition(Vector3 worldPos)
    {
        float x = (Mathf.Round((worldPos.x + 5) / 10)) * 10 - 5;
        float z = (Mathf.Round((worldPos.z + 5) / 10)) * 10 - 5;
        return new Vector3(x, transform.position.y, z);
    }

    public void Move(Vector3 targetPos, ChessPiece capturedPiece)
    {
        if (capturedPiece != null)
            capturedPiece.gameObject.SetActive(false);
        transform.position = targetPos;
        gameManager.SwitchTurn();
    }
}