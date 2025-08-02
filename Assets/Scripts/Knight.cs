using UnityEngine;

public class Knight : ChessPiece
{
    protected override bool IsLegalMovePattern(Vector3 from, Vector3 to)
    {
        float deltaX = Mathf.Abs(to.x - from.x);
        float deltaZ = Mathf.Abs(to.z - from.z);

        return (deltaX == 20 && deltaZ == 10) || (deltaX == 10 && deltaZ == 20);
    }
}