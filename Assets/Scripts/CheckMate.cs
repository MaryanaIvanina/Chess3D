using UnityEngine;
using System.Collections.Generic;

public class CheckMate : MonoBehaviour
{
    public bool isMate = false;
    private Vector3 direction = new Vector3(1, 0, 1);
    private Vector3 direction2 = new Vector3(-1, 0, 1);
    private Vector3 direction3 = new Vector3(1, 0, -1);
    private Vector3 direction4 = new Vector3(-1, 0, -1);
    private Vector3 direction5 = new Vector3(1, 0, 2);
    private Vector3 direction6 = new Vector3(-1, 0, 2);
    private Vector3 direction7 = new Vector3(-2, 0, 1);
    private Vector3 direction8 = new Vector3(-2, 0, -1);

    private Dictionary<string, int> pieceMap = new Dictionary<string, int>()
{
    {"BlackBishop", 1},
    {"BlackBishop (1)", 1},
    {"BlackQueen", 9},
    {"BlackRook", 2},
    {"BlackRook (1)", 2},
    {"BlackKnight", 3},
    {"BlackKnight (1)", 3},
    {"BlackPawn", 4},
    {"BlackPawn (1)", 4},
    {"BlackPawn (2)", 4},
    {"BlackPawn (3)", 4},
    {"BlackPawn (4)", 4},
    {"BlackPawn (5)", 4},
    {"BlackPawn (6)", 4},
    {"BlackPawn (7)", 4},
    {"Bishop", 5},
    {"Bishop (1)", 5},
    {"Queen", 10},
    {"Rook", 6},
    {"Rook (1)", 6},
    {"Knight", 7},
    {"Knight (1)", 7},
    {"Pawn", 8},
    {"Pawn (1)", 8},
    {"Pawn (2)", 8},
    {"Pawn (3)", 8},
    {"Pawn (4)", 8},
    {"Pawn (5)", 8},
    {"Pawn (6)", 8},
    {"Pawn (7)", 8}
};

    private int GetPieceCode(GameObject piece)
    {
        if (pieceMap.TryGetValue(piece.name, out int code))
            return code;
        else
            return 0;
    }

    private void CheckForMate()
    {
        Vector3 origin = transform.position + Vector3.up * 5;

        Ray[] rays = new Ray[]
        {
        new Ray(origin, direction),
        new Ray(origin, direction2),
        new Ray(origin, direction3),
        new Ray(origin, direction4),
        new Ray(origin, transform.forward),
        new Ray(origin, -transform.forward),
        new Ray(origin, transform.right),
        new Ray(origin, -transform.right),
        new Ray(origin, direction5),
        new Ray(origin, direction6),
        new Ray(origin, direction7),
        new Ray(origin, direction8),
        };

        isMate = false;

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], out RaycastHit hit))
            {
                int pieceCode = GetPieceCode(hit.collider.gameObject);

                if (gameObject.tag == "BlackKing")
                {
                    if ((i <= 3 && (pieceCode == 5 || pieceCode == 10)) ||
                        (i == 4 || i == 5 || i == 6 || i == 7) && (pieceCode == 6 || pieceCode == 10) ||
                        (i >= 8 && pieceCode == 7) ||
                        (pieceCode == 8 && IsPawnThreatening(hit.collider.gameObject.transform.position, 1)))
                    {
                        isMate = true;
                        break;
                    }
                }
                else if (gameObject.tag == "King")
                {
                    if ((i <= 3 && (pieceCode == 1 || pieceCode == 9)) ||
                        (i == 4 || i == 5 || i == 6 || i == 7) && (pieceCode == 2 || pieceCode == 9) ||
                        (i >= 8 && pieceCode == 3) ||
                        (pieceCode == 4 && IsPawnThreatening(hit.collider.gameObject.transform.position, -1)))
                    {
                        isMate = true;
                        break;
                    }
                }
            }
        }
    }

    private bool IsPawnThreatening(Vector3 pawnPosition, int direction)
    {
        float dx = Mathf.Abs(pawnPosition.x - transform.position.x);
        float dz = (pawnPosition.z - transform.position.z) * direction;
        return dx == 10f && dz == 10f;
    }
    void Update()
    {
        CheckForMate();
    }
}
