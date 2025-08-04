using UnityEngine;

public class PawnPromotionUI : MonoBehaviour
{
    public void PromoteToQueen()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Queen, PieceColor.White);
    }

    public void PromoteToRook()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Rook, PieceColor.White);
    }

    public void PromoteToBishop()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Bishop, PieceColor.White);
    }

    public void PromoteToKnight()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Knight, PieceColor.White);
    }

    public void PromoteToBlackQueen()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Queen, PieceColor.Black);
    }

    public void PromoteToBlackRook()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Rook, PieceColor.Black);
    }

    public void PromoteToBlackBishop()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Bishop, PieceColor.Black);
    }

    public void PromoteToBlackKnight()
    {
        ChessGameManager.Instance.PromotePawnTo(PieceType.Knight, PieceColor.Black);
    }
}