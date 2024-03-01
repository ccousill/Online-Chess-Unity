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
        Vector2Int forwardOneLeft = new Vector2Int(currentPosition.x - 1, currentPosition.y + forwardDirection);
         Vector2Int forwardOneRight = new Vector2Int(currentPosition.x + 1, currentPosition.y + forwardDirection);


        if (IsWithinBounds(forwardOne.x, forwardOne.y) && board.pieceBoard[forwardOne.x, forwardOne.y] == null)
        {
            if (IsWithinBounds(forwardTwo.x, forwardTwo.y) && board.pieceBoard[forwardTwo.x, forwardTwo.y] == null && !hasMoved)
            {
                availableMoves.Add(forwardTwo);
            }
            availableMoves.Add(forwardOne);
        }
        if (board.enPassantablePiece != null)
        {
            if (board.enPassantablePiece.currentPosition.y == currentPosition.y && (currentPosition.x == board.enPassantablePiece.currentPosition.x + 1 || currentPosition.x == board.enPassantablePiece.currentPosition.x - 1))
            {
                availableMoves.Add(new Vector2Int(board.enPassantablePiece.currentPosition.x, board.enPassantablePiece.currentPosition.y + forwardDirection));
            }
        }
        if (IsWithinBounds(forwardOneLeft.x, forwardOneLeft.y) && board.pieceBoard[forwardOneLeft.x, forwardOneLeft.y] != null && board.pieceBoard[forwardOneLeft.x, forwardOneLeft.y].team != team)
        {
            availableMoves.Add(forwardOneLeft);
        }
        if (IsWithinBounds(forwardOneRight.x, forwardOneRight.y) && board.pieceBoard[forwardOneRight.x, forwardOneRight.y] != null && board.pieceBoard[forwardOneRight.x, forwardOneRight.y].team != team)
        {
            availableMoves.Add(forwardOneRight);
        }

        return availableMoves;
    }

    public bool HasReachedEnd()
    {
        return (team == "White" && currentPosition.y == Chessboard.BoardSize-1) || (team == "Black" && currentPosition.y == 0);
    }
}
