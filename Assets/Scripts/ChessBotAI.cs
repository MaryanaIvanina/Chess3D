using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChessBotAI : MonoBehaviour
{
    public PieceColor botColor = PieceColor.Black;
    public int searchDepth = 1;

    private ChessGameManager gameManager;
    private bool isThinking = false;

    private struct MoveCandidate
    {
        public ChessPiece piece;
        public Vector3 from;
        public Vector3 to;
        public ChessPiece capturedPiece;
    }

    private class SimState
    {
        public MoveCandidate move;
        public Vector3 originalPos;
        public bool capturedWasActive;
    }

    private void Start()
    {
        gameManager = ChessGameManager.Instance;
    }

    private void Update()
    {
        if (gameManager == null) return;

        if (gameManager.currentTurn == botColor && !isThinking)
        {
            Physics.SyncTransforms();
            MakeBestMove();
        }
    }

    private void MakeBestMove()
    {
        isThinking = true;

        List<MoveCandidate> moves = GenerateAllLegalMoves(botColor);


        if (moves.Count == 0)
        {
            isThinking = false;
            return;
        }

        MoveCandidate best = moves[0];
        float bestValue = float.NegativeInfinity;

        foreach (var m in moves)
        {
            Debug.Log($""+m.piece.pieceColor + " " + m.piece.pieceType + " ");
            ApplyMoveSimulated(m, out SimState simState);

            float value = Minimax(simState, searchDepth - 1, botColor == PieceColor.White ? true : false, float.NegativeInfinity, float.PositiveInfinity);

            UndoSimulated(simState);

            if (value > bestValue)
            {
                bestValue = value;
                best = m;
            }
        }

        ExecuteRealMove(best);

        isThinking = false;
    }

    private List<MoveCandidate> GenerateAllLegalMoves(PieceColor color)
    {
        List<MoveCandidate> list = new List<MoveCandidate>();

        ChessPiece[] pieces = gameManager.GetPiecesOfColor(color);

        foreach (var piece in pieces)
        {
            if (!piece.gameObject.activeInHierarchy) continue;

            for (int i = 5; i <= 75; i += 10)
            {
                for (int j = 5; j <= 75; j += 10)
                {
                    Vector3 targetPos = new Vector3(j, 5, i);

                    if (piece.IsValidMove(piece.transform.position, targetPos, color) &&
                    !gameManager.WouldKingBeInCheck(color, piece, targetPos))
                    {
                        var capt = piece.GetPieceAtPosition(targetPos);
                        list.Add(new MoveCandidate
                        {
                            piece = piece,
                            from = piece.transform.position,
                            to = targetPos,
                            capturedPiece = capt
                        });
                    }
                }
            }
        }
        return list;
    }

    private void ApplyMoveSimulated(MoveCandidate move, out SimState state)
    {
        state = new SimState();
        state.move = move;
        state.originalPos = move.piece.transform.position;
        state.capturedWasActive = move.capturedPiece != null ? move.capturedPiece.gameObject.activeInHierarchy : false;

        move.piece.transform.position = move.to;

        if (move.capturedPiece != null) move.capturedPiece.gameObject.SetActive(false);

        Physics.SyncTransforms();
    }

    private void UndoSimulated(SimState state)
    {
        state.move.piece.transform.position = state.originalPos;

        if (state.move.capturedPiece != null) 
            state.move.capturedPiece.gameObject.SetActive(state.capturedWasActive);

        Canvas.ForceUpdateCanvases();
        Physics.SyncTransforms();
    }

    private float Minimax(SimState simState, int depth, bool isMaximizing, float alpha, float beta)
    {
        if (depth == 0) return EvaluateBoardForBot();

        PieceColor sideToMove = isMaximizing ? botColor : (botColor == PieceColor.White ? PieceColor.Black : PieceColor.White);

        List<MoveCandidate> moves = GenerateAllLegalMoves(sideToMove);
        

        if (moves.Count == 0)
        {
            bool inCheck = gameManager.IsKingInCheck(sideToMove);
            if (inCheck)
            {
                if (sideToMove == botColor) return float.NegativeInfinity;
                else return float.PositiveInfinity;
            }
            else return 0f;
        }

        if (isMaximizing)
        {
            float maxEval = float.NegativeInfinity;
            foreach (var m in moves)
            {
                ApplyMoveSimulated(m, out SimState s);
                float eval = Minimax(s, depth - 1, false, alpha, beta);
                UndoSimulated(s);

                if (eval > maxEval) maxEval = eval;
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha) break;
            }
            return maxEval;
        }
        else
        {
            float minEval = float.PositiveInfinity;
            foreach (var m in moves)
            {
                ApplyMoveSimulated(m, out SimState s);
                float eval = Minimax(s, depth - 1, true, alpha, beta);
                UndoSimulated(s);

                if (eval < minEval) minEval = eval;
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha) break;
            }
            return minEval;
        }
    }

    private float EvaluateBoardForBot()
    {
        float score = 0f;

        ChessPiece[] allPieces = FindObjectsByType<ChessPiece>(FindObjectsSortMode.None);
        int whiteMaterial = 0;
        int blackMaterial = 0;

        foreach (var p in allPieces)
        {
            if (!p.gameObject.activeInHierarchy) continue;

            int val = GetPieceValue(p.pieceType);
            if (p.pieceColor == PieceColor.White) whiteMaterial += val;
            else blackMaterial += val;
        }

        score = botColor == PieceColor.White ? (whiteMaterial - blackMaterial) : (blackMaterial - whiteMaterial);

        int botMoves = GenerateAllLegalMoves(botColor).Count;
        int oppMoves = GenerateAllLegalMoves(botColor == PieceColor.White ? PieceColor.Black : PieceColor.White).Count;
        score += 0.1f * (botMoves - oppMoves);

        return score;
    }

    private int GetPieceValue(PieceType type)
    {
        switch (type)
        {
            case PieceType.Pawn: return 100;
            case PieceType.Knight: return 320;
            case PieceType.Bishop: return 330;
            case PieceType.Rook: return 500;
            case PieceType.Queen: return 900;
            case PieceType.King: return 20000;
            default: return 0;
        }
    }

    private void ExecuteRealMove(MoveCandidate move)
    {
        if (move.piece == null || !move.piece.gameObject.activeInHierarchy)
            return;

        ChessPiece captured = move.piece.GetPieceAtPosition(move.to);

        move.piece.Move(move.to, captured);
    }
}
