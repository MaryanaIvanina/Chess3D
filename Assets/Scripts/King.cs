using UnityEngine;
using Photon.Pun;

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
        if ((deltaX == 20 && deltaZ == 0) && !hasKingMoved)
        {
            if (from.x > to.x)
                isCastling = IsCastlingPossible(from, to - Vector3.right * 10, to - Vector3.right * 20, to + Vector3.right * 10);
            else
                isCastling = IsCastlingPossible(from, to, to + Vector3.right * 10, to - Vector3.right * 10);
        }

        return (deltaX <= 10 && deltaZ <= 10 && (deltaX + deltaZ > 0)) || isCastling;
    }

    protected override void ExecuteMove(Vector3 targetPos)
    {
        base.ExecuteMove(targetPos);
        hasKingMoved = true;

        if (isCastling)
            castlingRook.transform.position = castlingRookPosition;

        if (GameMode.Instance.gameMode == 3 && PhotonNetwork.IsConnected)
        {
            PhotonView rookView = castlingRook.GetComponent<PhotonView>();
            if (rookView != null)
                photonView.RPC("RPC_Castling", RpcTarget.All, rookView.ViewID, castlingRookPosition);
        }

        isCastling = false;
        castlingRook = null;
    }

    private bool IsCastlingPossible(Vector3 kingPos, Vector3 blockingPos, Vector3 rookPos, Vector3 futureRookPos)
    {
        isCastlingPathBlocked = IsPathBlocked(kingPos, blockingPos);
        castlingRook = GetPieceAtPosition(rookPos);
        if (castlingRook == null) return false;
        castlingRookPosition = futureRookPos;
        rookMoved = castlingRook != null && castlingRook.hasRookMoved;
        if (rookMoved || gameManager.IsKingInCheck(pieceColor) || isCastlingPathBlocked || castlingRook.pieceType != PieceType.Rook)
            return false;
        return true;
    }
}