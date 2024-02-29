using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        int forwardDirection = (team == "White") ? 1 : -1;
        Vector2Int forwardOne = new Vector2Int(currentPosition.x, currentPosition.y + forwardDirection);
        Vector2Int forwardTwo = new Vector2Int(currentPosition.x, currentPosition.y + (2 * forwardDirection));
        // Vector2Int forwardOneLeft = new Vector2Int(currentPosition.x - 1, currentPosition.y + forwardDirection);
        // Vector2Int forwardOneRight = new Vector2Int(currentPosition.x + 1, currentPosition.y + forwardDirection);


        if (IsWithinBounds(forwardOne.x, forwardOne.y) && board.pieceBoard[forwardOne.x, forwardOne.y] == null)
        {
            if (IsWithinBounds(forwardTwo.x, forwardTwo.y) && board.pieceBoard[forwardTwo.x, forwardTwo.y] == null && CanMoveDouble())
            {
                availableMoves.Add(forwardTwo);
            }
            availableMoves.Add(forwardOne);
        }
        return availableMoves;
    }

    public bool CanMoveDouble()
    {
        if (team == "White")
        {
            return currentPosition.y < 2;
        }
        else if (team == "Black")
        {
            return currentPosition.y > 5;
        }
        return false;
    }
}
