using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector2Int> SelectAvailableSquares()
    {
        availableMoves.Clear();
        int xPiecePosition = currentPosition.x;
        int yPiecePosition = currentPosition.y;
        CheckAround(xPiecePosition, yPiecePosition);
        return availableMoves;
    }
    void CheckAround(int xPiecePosition, int yPiecePosition)
    {
        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                int x = xPiecePosition + xOffset;
                int y = yPiecePosition + yOffset;

                if (IsWithinBounds(x, y))
                {
                    Piece targetPiece = board.GetPieceOnSquare(new Vector2Int(x, y));

                    // Check if the spot is empty or contains an opponent's piece
                    if (targetPiece == null || board.IsTakablePiece(this,targetPiece))
                    {
                        // Process the tile or make it available
                        availableMoves.Add(new Vector2Int(x, y));
                    }
                }
            }
        }
    }
}
