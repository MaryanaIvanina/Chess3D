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

        if (deltaX == 0 && targetPiece == null)
            return (deltaZ == direction) || (deltaZ == 2 * direction && moveCount == 0);
        else if (deltaX == 10 && deltaZ == direction)
            return targetPiece != null && targetPiece.pieceColor != pieceColor;
        else return false;
    }

    protected override void ExecuteMove(Vector3 targetPos)
    {
        base.ExecuteMove(targetPos);
        moveCount++;

        float endRow = pieceColor == PieceColor.White ? 75f : 5f;
        if (Mathf.Approximately(transform.position.z, endRow))
            gameManager.StartPawnPromotion(this);
    }
}