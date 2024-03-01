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
        if (!hasMoved)
        {
            CheckCastle(xPiecePosition);
        }
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

    void CheckCastle(int xPiecePosition)
    {
        bool isBlocked = false;
        int leftRookX = 0;
        int rightRookX = 7;
        for (int i = xPiecePosition - 1; i > leftRookX; i--)
        {
            if (board.GetPieceOnSquare(new Vector2Int(i, currentPosition.y)) != null)
            {
                isBlocked = true;
            }
        }
        Piece leftRook = board.GetPieceOnSquare(new Vector2Int(leftRookX,currentPosition.y));
        if(!isBlocked && leftRook is Rook && !leftRook.hasMoved){
            availableMoves.Add(new Vector2Int(currentPosition.x - 2,currentPosition.y));
        }

        isBlocked = false;
        for (int i = xPiecePosition + 1; i < rightRookX ; i++)
        {
            if (board.GetPieceOnSquare(new Vector2Int(i, currentPosition.y)) != null)
            {
                isBlocked = true;
            }
        }
        Piece rightRook = board.GetPieceOnSquare(new Vector2Int(rightRookX,currentPosition.y));
        if(!isBlocked && rightRook is Rook && !rightRook.hasMoved){
            availableMoves.Add(new Vector2Int(currentPosition.x + 2,currentPosition.y));
        }
    }
}
