using UnityEngine;

public class Bishop : ChessPiece
{
    protected override bool IsLegalMovePattern(Vector3 from, Vector3 to)
    {
        float deltaX = Mathf.Abs(to.x - from.x);
        float deltaZ = Mathf.Abs(to.z - from.z);

        return deltaX == deltaZ && deltaX > 0;
    }
}