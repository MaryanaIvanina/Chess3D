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

    [Header("Promotion UI")]
    public GameObject whitePawnPromotionUI;
    public GameObject blackPawnPromotionUI;

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

        if (whitePawnPromotionUI) whitePawnPromotionUI.SetActive(false);
        if (blackPawnPromotionUI) blackPawnPromotionUI.SetActive(false);
    }

    public void SwitchTurn()
    {
        currentTurn = currentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
        SetTurn(currentTurn);
        CheckForCheckmate();
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
                        Debug.Log($"{piece.pieceColor} {piece} is valid.");
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
        Vector3 piecePos = piece.transform.position;
        if (piece.pieceType == PieceType.Pawn)
            return CanPawnAttack(piecePos, targetPos, piece.pieceColor);
        else
            return piece.IsValidMove(piecePos, targetPos, piece.pieceColor);
    }

    private bool CanPawnAttack(Vector3 from, Vector3 to, PieceColor pawnColor)
    {
        float direction = pawnColor == PieceColor.White ? 10 : -10;
        float deltaZ = to.z - from.z;
        float deltaX = Mathf.Abs(to.x - from.x);

        return deltaX == 10 && deltaZ == direction;
    }

    public bool WouldKingBeInCheck(PieceColor kingColor, ChessPiece movingPiece, Vector3 toPos)
    {
        ChessPiece capturedPiece = null;
        ChessPiece[] allPieces = FindObjectsByType<ChessPiece>(FindObjectsSortMode.None);

        foreach (ChessPiece piece in allPieces)
        {
            if (Vector3.Distance(piece.transform.position, toPos) < 5f &&
                piece != movingPiece && piece.gameObject.activeInHierarchy)
            {
                capturedPiece = piece;
                break;
            }
        }

        if (capturedPiece != null && capturedPiece.pieceColor == kingColor)
            return false;

        Vector3 originalPos = movingPiece.transform.position;

        movingPiece.transform.position = toPos;
        if (capturedPiece != null)
            capturedPiece.gameObject.SetActive(false);

        Canvas.ForceUpdateCanvases();
        Physics.SyncTransforms();

        bool wouldBeInCheck = IsKingInCheck(kingColor);

        movingPiece.transform.position = originalPos;
        if (capturedPiece != null)
            capturedPiece.gameObject.SetActive(true);

        return wouldBeInCheck;
    }

    public void StartPawnPromotion(Pawn pawn)
    {
        pawnToPromote = pawn;

        // Показуємо відповідний UI в залежності від кольору пішака
        if (pawn.pieceColor == PieceColor.White)
            if (whitePawnPromotionUI) whitePawnPromotionUI.SetActive(true);
        else
            if (blackPawnPromotionUI) blackPawnPromotionUI.SetActive(true);

        // Зупиняємо гру до вибору фігури
        Time.timeScale = 0f;
    }

    public void PromotePawnTo(PieceType newPieceType)
    {
        if (pawnToPromote == null) return;

        Vector3 position = pawnToPromote.transform.position;
        Quaternion rotation = pawnToPromote.transform.rotation;
        PieceColor color = pawnToPromote.pieceColor;

        Destroy(pawnToPromote.gameObject);

        GameObject prefab = GetPrefabForPieceType(newPieceType);
        if (prefab != null)
        {
            GameObject newPiece = Instantiate(prefab, position, rotation);
            ChessPiece piece = newPiece.GetComponent<ChessPiece>();
            piece.pieceColor = color;
            piece.pieceType = newPieceType;

            if (newPieceType == PieceType.Knight)
            {
                if (color == PieceColor.Black)
                    newPiece.transform.Rotate(0, -180f, 0);
            }
        }

        // Ховаємо UI
        if (whitePawnPromotionUI) whitePawnPromotionUI.SetActive(false);
        if (blackPawnPromotionUI) blackPawnPromotionUI.SetActive(false);

        // Відновлюємо час гри
        Time.timeScale = 1f;
        pawnToPromote = null;
    }

    private GameObject GetPrefabForPieceType(PieceType type)
    {
        switch (type)
        {
            case PieceType.Queen: return queenPrefab;
            case PieceType.Rook: return rookPrefab;
            case PieceType.Bishop: return bishopPrefab;
            case PieceType.Knight: return knightPrefab;
            default: return queenPrefab;
        }
    }
}