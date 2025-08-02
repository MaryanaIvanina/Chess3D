using UnityEngine;

public class Rook : ChessPiece
{
    protected override bool IsLegalMovePattern(Vector3 from, Vector3 to)
    {
        if (from.x == to.x || from.z == to.z) return true;
        return false;
    }
}