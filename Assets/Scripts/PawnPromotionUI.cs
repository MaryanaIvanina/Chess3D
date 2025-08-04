using UnityEngine;

public class PawnPromotionUI : MonoBehaviour
{
    public void PromoteToQueen()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Queen);
    }

    public void PromoteToRook()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Rook);
    }

    public void PromoteToBishop()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Bishop);
    }

    public void PromoteToKnight()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Knight);
    }
}