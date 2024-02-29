using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Knight : Piece
{
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        int xPiecePosition = currentPosition.x;
        int yPiecePosition = currentPosition.y;
        CheckDirection(xPiecePosition, yPiecePosition, 1, 1); // Check up right
        CheckDirection(xPiecePosition, yPiecePosition, 1, -1); // Check  down right
        CheckDirection(xPiecePosition, yPiecePosition, -1, -1); // Check down left
        CheckDirection(xPiecePosition, yPiecePosition, -1, 1); // check up left
        return availableMoves;
    }

    private void CheckDirection(int x, int y, int xDirection, int yDirection)
    {
        int newX = x + xDirection;
        int newY = y + yDirection * 2;
        if(IsWithinBounds(newX,newY)){
            availableMoves.Add(new Vector2Int(newX,newY));
        }
        newX = x + xDirection * 2;
        newY = y + yDirection;
        if(IsWithinBounds(newX,newY)){
            availableMoves.Add(new Vector2Int(newX,newY));
        }
    }
}
