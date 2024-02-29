using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        int xPiecePosition = currentPosition.x;
        int yPiecePosition = currentPosition.y;
        CheckDirection(xPiecePosition, yPiecePosition, 1, 1); // Check up right
        CheckDirection(xPiecePosition, yPiecePosition, -1, 1); // Check up left
        CheckDirection(xPiecePosition, yPiecePosition, 1, -1); // Check down right
        CheckDirection(xPiecePosition, yPiecePosition, -1, -1); //check down left
        return availableMoves;
    }

    private void CheckDirection(int x, int y, int xDirection, int yDirection)
    {
        x += xDirection;
        y += yDirection;

        while (IsWithinBounds(x, y))
        {
            if (board.GetPieceOnSquare(new Vector2Int(x, y)) == null)
            {
                availableMoves.Add(new Vector2Int(x, y));
                x += xDirection;
                y += yDirection;
            }
            else if (board.IsTakablePiece(this,board.GetPieceOnSquare(new Vector2Int(x, y))))
            {
                availableMoves.Add(new Vector2Int(x, y));
                break;
            }
            else
            {
                break;
            }

        }
    }
}
