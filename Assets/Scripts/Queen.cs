using UnityEngine;

public class Queen : ChessPiece
{
    protected override bool IsLegalMovePattern(Vector3 from, Vector3 to)
    {
        float deltaX = Mathf.Abs(to.x - from.x);
        float deltaZ = Mathf.Abs(to.z - from.z);

        return (from.x == to.x || from.z == to.z) ||
               (deltaX == deltaZ && deltaX > 0);
    }
}