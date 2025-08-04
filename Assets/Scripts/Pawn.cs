using UnityEngine;

public class Pawn : ChessPiece
{
    private int moveCount = 0;

    protected override bool IsLegalMovePattern(Vector3 from, Vector3 to)
    {
        float direction = pieceColor == PieceColor.White ? 10 : -10;
        float deltaZ = to.z - from.z;
        float deltaX = Mathf.Abs(to.x - from.x);

        ChessPiece targetPiece = GetPieceAtPosition(to);

        // Рух вперед (без захоплення)
        if (deltaX == 0 && targetPiece == null)
        {
            if (deltaZ == direction) return true;
            if (deltaZ == 2 * direction && moveCount == 0) return true;
        }
        // Захоплення по діагоналі
        else if (deltaX == 10 && deltaZ == direction)
            return targetPiece != null && targetPiece.pieceColor != pieceColor;

        return false;
    }

    private ChessPiece GetPieceAtPosition(Vector3 position)
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

    protected override void ExecuteMove(Vector3 targetPos)
    {
        base.ExecuteMove(targetPos);
        moveCount++;

        // Перевіряємо превращення пішака
        float endRow = pieceColor == PieceColor.White ? 75f : 5f;
        if (Mathf.Approximately(transform.position.z, endRow))
            gameManager.StartPawnPromotion(this);
    }
}