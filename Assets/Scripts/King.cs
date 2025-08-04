using UnityEngine;

public class King : ChessPiece
{
    private bool hasKingMoved = false;
    public bool isCastling = false;
    private bool isCastlingPathBlocked = false;
    private ChessPiece castlingRook;
    private Vector3 castlingRookPosition;
    private bool rookMoved = false;

    protected override void Start()
    {
        base.Start();
        castlingRook = null;
    }

    protected override bool IsLegalMovePattern(Vector3 from, Vector3 to)
    {
        float deltaX = Mathf.Abs(to.x - from.x);
        float deltaZ = Mathf.Abs(to.z - from.z);
        if (!hasKingMoved)
        {
            if (from.x > to.x)
                IsCastlingPossible(from, to - Vector3.right * 10, to - Vector3.right * 20, to + Vector3.right * 10);
            else
                IsCastlingPossible(from, to, to + Vector3.right * 10, to - Vector3.right * 10);

            isCastling = (deltaX == 20 && deltaZ == 0) && castlingRook != null && !rookMoved && 
                !gameManager.IsKingInCheck(pieceColor) && !isCastlingPathBlocked;
        }

        return (deltaX <= 10 && deltaZ <= 10 && (deltaX + deltaZ > 0)) || isCastling;
    }

    protected override void ExecuteMove(Vector3 targetPos)
    {
        base.ExecuteMove(targetPos);
        hasKingMoved = true;

        if (isCastling)
            castlingRook.transform.position = castlingRookPosition;
        isCastling = false;
        castlingRook = null;
    }

    private void IsCastlingPossible(Vector3 kingPos, Vector3 blockingPos, Vector3 rookPos, Vector3 futureRookPos)
    {
        isCastlingPathBlocked = IsPathBlocked(kingPos, blockingPos);
        castlingRook = GetPieceAtPosition(rookPos);
        castlingRookPosition = futureRookPos;
        rookMoved = castlingRook != null && castlingRook.hasRookMoved;
    }
}