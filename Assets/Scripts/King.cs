using UnityEngine;

public class King : ChessPiece
{
    protected override bool IsLegalMovePattern(Vector3 from, Vector3 to)
    {
        float deltaX = Mathf.Abs(to.x - from.x);
        float deltaZ = Mathf.Abs(to.z - from.z);

        return deltaX <= 10 && deltaZ <= 10 && (deltaX + deltaZ > 0);
    }

    public override bool IsValidMove(Vector3 from, Vector3 to)
    {
        if (!base.IsValidMove(from, to)) return false;

        return !gameManager.WouldKingBeInCheck(pieceColor, this, to);
    }
}