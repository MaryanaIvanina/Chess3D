using UnityEngine;
using System.Collections.Generic;

public class ChessGameManager : MonoBehaviour
{
    public static ChessGameManager Instance { get; private set; }

    [Header("Game State")]
    public PieceColor currentTurn = PieceColor.White;
    public bool isInCheck = false;

    [Header("Promotion")]
    public GameObject queenPrefab;
    public GameObject rookPrefab;
    public GameObject bishopPrefab;
    public GameObject knightPrefab;
    public GameObject blackQueenPrefab;
    public GameObject blackRookPrefab;
    public GameObject blackBishopPrefab;
    public GameObject blackKnightPrefab;

    [Header("Promotion UI")]
    public GameObject whitePawnPromotionUI;
    public GameObject blackPawnPromotionUI;

    [Header("Camera Switch")]
    [SerializeField] private SwitchCamera _switchCamera;

    private Pawn pawnToPromote;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetTurn(PieceColor.White);
        HidePromotionUI();
    }

    public void SwitchTurn()
    {
        currentTurn = currentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
        SetTurn(currentTurn);
        CheckForCheckmate();
        if (currentTurn == PieceColor.White) _switchCamera.cameraIndex = 0;
        else _switchCamera.cameraIndex = 1;
        _switchCamera.SwitchCameraPosition();
    }

    private void SetTurn(PieceColor color)
    {
        ChessPiece[] allPieces = FindObjectsByType<ChessPiece>(FindObjectsSortMode.None);

        foreach (ChessPiece piece in allPieces)
        {
            piece.enabled = (piece.pieceColor == color);
        }
    }

    private void CheckForCheckmate()
    {
        isInCheck = IsKingInCheck(currentTurn);

        if (isInCheck)
        {
            if (!HasAnyValidMove())
                Debug.Log($"{currentTurn} is in checkmate!");
            else
                Debug.Log($"{currentTurn} king is in check!");
        }
        else
        {
            if (!HasAnyValidMove())
                Debug.Log($"Stalemate! {currentTurn} has no valid moves but is not in check.");
        }
    }

    private bool HasAnyValidMove()
    {
        ChessPiece[] alliedPieces = GetPiecesOfColor(currentTurn);
        foreach (ChessPiece piece in alliedPieces)
        {
            for (int i = 5; i <= 75; i += 10)
            {
                for (int j = 5; j <= 75; j += 10)
                {
                    Vector3 targetPos = new Vector3(j, 5, i);
                    if (piece.IsValidMove(piece.transform.position, targetPos, currentTurn) &&
                        !WouldKingBeInCheck(currentTurn, piece, targetPos))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool IsKingInCheck(PieceColor kingColor)
    {
        King king = FindKing(kingColor);
        if (king == null) return false;

        return IsPositionUnderAttack(king.transform.position, kingColor);
    }

    private King FindKing(PieceColor color)
    {
        King[] kings = FindObjectsByType<King>(FindObjectsSortMode.None);
        foreach (King king in kings)
        {
            if (king.pieceColor == color && king.gameObject.activeInHierarchy) return king;
        }
        return null;
    }

    private bool IsPositionUnderAttack(Vector3 position, PieceColor defendingColor)
    {
        ChessPiece[] enemyPieces = GetPiecesOfColor(defendingColor == PieceColor.White ? PieceColor.Black : PieceColor.White);

        foreach (ChessPiece piece in enemyPieces)
        {
            if (piece.gameObject.activeInHierarchy && CanPieceAttackPosition(piece, position))
                return true;
        }

        return false;
    }

    public ChessPiece[] GetPiecesOfColor(PieceColor color)
    {
        ChessPiece[] allPieces = FindObjectsByType<ChessPiece>(FindObjectsSortMode.None);
        List<ChessPiece> colorPieces = new List<ChessPiece>();

        foreach (ChessPiece piece in allPieces)
        {
            if (piece.pieceColor == color) colorPieces.Add(piece);
        }

        return colorPieces.ToArray();
    }

    private bool CanPieceAttackPosition(ChessPiece piece, Vector3 targetPos)
    {
        return piece.IsValidMove(piece.transform.position, targetPos, piece.pieceColor);
    }

    public bool WouldKingBeInCheck(PieceColor kingColor, ChessPiece movingPiece, Vector3 toPos)
    {
        ChessPiece capturedPiece = movingPiece.GetPieceAtPosition(toPos);
        Vector3 originalPos = movingPiece.transform.position;

        movingPiece.transform.position = toPos;
        if (capturedPiece != null) capturedPiece.gameObject.SetActive(false);

        Canvas.ForceUpdateCanvases();
        Physics.SyncTransforms();

        bool wouldBeInCheck = IsKingInCheck(kingColor);

        movingPiece.transform.position = originalPos;
        if (capturedPiece != null) capturedPiece.gameObject.SetActive(true);

        return wouldBeInCheck;
    }

    public void StartPawnPromotion(Pawn pawn)
    {
        pawnToPromote = pawn;

        if (pawn.pieceColor == PieceColor.White)
            whitePawnPromotionUI.SetActive(true);
        else
            blackPawnPromotionUI.SetActive(true);

    }

    public void PromotePawnTo(PieceType newPieceType, PieceColor newPieceColor)
    {
        if (pawnToPromote == null) return;

        Vector3 position = pawnToPromote.transform.position;
        Quaternion rotation = pawnToPromote.transform.rotation;
        PieceColor color = pawnToPromote.pieceColor;

        Destroy(pawnToPromote.gameObject);

        GameObject prefab = GetPrefabForPieceType(newPieceType, newPieceColor);
        if (prefab != null)
        {
            GameObject newPiece = Instantiate(prefab, position, rotation);
            ChessPiece piece = newPiece.GetComponent<ChessPiece>();
            piece.pieceColor = newPieceColor;
            piece.pieceType = newPieceType;
        }
        HidePromotionUI();
        pawnToPromote = null;
    }

    private GameObject GetPrefabForPieceType(PieceType type, PieceColor color)
    {
        switch (type)
        {
            case PieceType.Queen:
                if (color == PieceColor.White) return queenPrefab;
                else return blackQueenPrefab;
            case PieceType.Rook:
                if (color == PieceColor.White) return rookPrefab;
                else return blackRookPrefab;
            case PieceType.Bishop:
                if (color == PieceColor.White) return bishopPrefab;
                else return blackBishopPrefab;
            case PieceType.Knight:
                if (color == PieceColor.White) return knightPrefab;
                else return blackKnightPrefab;
            default: return queenPrefab;
        }
    }

    private void HidePromotionUI()
    {
        whitePawnPromotionUI.SetActive(false);
        blackPawnPromotionUI.SetActive(false);
    }
}