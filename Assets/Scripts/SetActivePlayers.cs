using System.Collections;
using UnityEngine;

public class SetActivePlayers : MonoBehaviour
{
    public void SetActiveWhite()
    {
        if (gameObject.name == "Pawn" || gameObject.name == "Pawn (1)" || gameObject.name == "Pawn (2)" || gameObject.name == "Pawn (3)" || gameObject.name == "Pawn (4)" || gameObject.name == "Pawn (5)" || gameObject.name == "Pawn (6)" || gameObject.name == "Pawn (7)")
        {
            Pawn pawn = GetComponent<Pawn>();
            pawn.enabled = false;
        }
        else if (gameObject.name == "Rook" || gameObject.name == "Rook (1)")
        {
            Rook rook = GetComponent<Rook>();
            rook.enabled = false;
        }
        else if (gameObject.name == "Knight" || gameObject.name == "Knight (1)")
        {
            Knight knight = GetComponent<Knight>();
            knight.enabled = false;
        }
        else if (gameObject.name == "Bishop" || gameObject.name == "Bishop (1)")
        {
            Bishop bishop = GetComponent<Bishop>();
            bishop.enabled = false;
        }
        else if (gameObject.name == "Queen")
        {
            Queen queen = GetComponent<Queen>();
            queen.enabled = false;
        }
        else if (gameObject.name == "King")
        {
            King king = GetComponent<King>();
            king.enabled = false;
        }
        else if (gameObject.name == "Queen(Clone)")
        {
            Queen queen = GetComponent<Queen>();
            queen.enabled = false;
        }
        else if (gameObject.name == "Bishop(Clone)")
        {
            Bishop bishop = GetComponent<Bishop>();
            bishop.enabled = false;
        }
        else if (gameObject.name == "Knight(Clone)")
        {
            Knight knight = GetComponent<Knight>();
            knight.enabled = false;
        }
        else if (gameObject.name == "Rook(Clone)")
        {
            Rook rook = GetComponent<Rook>();
            rook.enabled = false;
        }
        else if (gameObject.name == "BlackPawn" || gameObject.name == "BlackPawn (1)" || gameObject.name == "BlackPawn (2)" || gameObject.name == "BlackPawn (3)" || gameObject.name == "BlackPawn (4)" || gameObject.name == "BlackPawn (5)" || gameObject.name == "BlackPawn (6)" || gameObject.name == "BlackPawn (7)")
        {
            BlackPawn blackPawn = GetComponent<BlackPawn>();
            blackPawn.enabled = true;
        }
        else if (gameObject.name == "BlackRook" || gameObject.name == "BlackRook (1)")
        {
            Rook blackRook = GetComponent<Rook>();
            blackRook.enabled = true;
        }
        else if (gameObject.name == "BlackKnight" || gameObject.name == "BlackKnight (1)")
        {
            Knight blackKnight = GetComponent<Knight>();
            blackKnight.enabled = true;
        }
        else if (gameObject.name == "BlackBishop" || gameObject.name == "BlackBishop (1)")
        {
            Bishop blackBishop = GetComponent<Bishop>();
            blackBishop.enabled = true;
        }
        else if (gameObject.name == "BlackQueen")
        {
            Queen blackQueen = GetComponent<Queen>();
            blackQueen.enabled = true;
        }
        else if (gameObject.name == "BlackKing")
        {
            King blackKing = GetComponent<King>();
            blackKing.enabled = true;
        }
        else if (gameObject.name == "BlackQueen(Clone)")
        {
            Queen blackQueen = GetComponent<Queen>();
            blackQueen.enabled = true;
        }
        else if (gameObject.name == "BlackBishop(Clone)")
        {
            Bishop blackBishop = GetComponent<Bishop>();
            blackBishop.enabled = true;
        }
        else if (gameObject.name == "BlackKnight(Clone)")
        {
            Knight blackKnight = GetComponent<Knight>();
            blackKnight.enabled = true;
        }
        else if (gameObject.name == "BlackRook(Clone)")
        {
            Rook blackRook = GetComponent<Rook>();
            blackRook.enabled = true;
        }
    }
    public void SetActiveBlack()
    {
        if (gameObject.name == "BlackPawn" || gameObject.name == "BlackPawn (1)" || gameObject.name == "BlackPawn (2)" || gameObject.name == "BlackPawn (3)" || gameObject.name == "BlackPawn (4)" || gameObject.name == "BlackPawn (5)" || gameObject.name == "BlackPawn (6)" || gameObject.name == "BlackPawn (7)")
        {
            BlackPawn blackPawn = GetComponent<BlackPawn>();
            blackPawn.enabled = false;
        }
        else if (gameObject.name == "BlackRook" || gameObject.name == "BlackRook (1)")
        {
            Rook blackRook = GetComponent<Rook>();
            blackRook.enabled = false;
        }
        else if (gameObject.name == "BlackKnight" || gameObject.name == "BlackKnight (1)")
        {
            Knight blackKnight = GetComponent<Knight>();
            blackKnight.enabled = false;
        }
        else if (gameObject.name == "BlackBishop" || gameObject.name == "BlackBishop (1)")
        {
            Bishop blackBishop = GetComponent<Bishop>();
            blackBishop.enabled = false;
        }
        else if (gameObject.name == "BlackQueen")
        {
            Queen blackQueen = GetComponent<Queen>();
            blackQueen.enabled = false;
        }
        else if (gameObject.name == "BlackKing")
        {
            King blackKing = GetComponent<King>();
            blackKing.enabled = false;
        }
        else if (gameObject.name == "BlackQueen(Clone)")
        {
            Queen blackQueen = GetComponent<Queen>();
            blackQueen.enabled = false;
        }
        else if (gameObject.name == "BlackBishop(Clone)")
        {
            Bishop blackBishop = GetComponent<Bishop>();
            blackBishop.enabled = false;
        }
        else if (gameObject.name == "BlackKnight(Clone)")
        {
            Knight blackKnight = GetComponent<Knight>();
            blackKnight.enabled = false;
        }
        else if (gameObject.name == "BlackRook(Clone)")
        {
            Rook blackRook = GetComponent<Rook>();
            blackRook.enabled = false;
        }
        else if (gameObject.name == "Pawn" || gameObject.name == "Pawn (1)" || gameObject.name == "Pawn (2)" || gameObject.name == "Pawn (3)" || gameObject.name == "Pawn (4)" || gameObject.name == "Pawn (5)" || gameObject.name == "Pawn (6)" || gameObject.name == "Pawn (7)")
        {
            Pawn pawn = GetComponent<Pawn>();
            pawn.enabled = true;
        }
        else if (gameObject.name == "Rook" || gameObject.name == "Rook (1)")
        {
            Rook rook = GetComponent<Rook>();
            rook.enabled = true;
        }
        else if (gameObject.name == "Knight" || gameObject.name == "Knight (1)")
        {
            Knight knight = GetComponent<Knight>();
            knight.enabled = true;
        }
        else if (gameObject.name == "Bishop" || gameObject.name == "Bishop (1)")
        {
            Bishop bishop = GetComponent<Bishop>();
            bishop.enabled = true;
        }
        else if (gameObject.name == "Queen")
        {
            Queen queen = GetComponent<Queen>();
            queen.enabled = true;
        }
        else if (gameObject.name == "King")
        {
            King king = GetComponent<King>();
            king.enabled = true;
        }
        else if (gameObject.name == "Queen(Clone)")
        {
            Queen queen = GetComponent<Queen>();
            queen.enabled = true;
        }
        else if (gameObject.name == "Bishop(Clone)")
        {
            Bishop bishop = GetComponent<Bishop>();
            bishop.enabled = true;
        }
        else if (gameObject.name == "Knight(Clone)")
        {
            Knight knight = GetComponent<Knight>();
            knight.enabled = true;
        }
        else if (gameObject.name == "Rook(Clone)")
        {
            Rook rook = GetComponent<Rook>();
            rook.enabled = true;
        }
    }
}
