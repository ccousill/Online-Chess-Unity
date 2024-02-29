using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Chessboard : MonoBehaviour
{
    public Tile[,] tileBoard { get; set; }
    public Piece[,] pieceBoard { get; set; }
    public const int BoardSize = 8;
    private Piece selectedPiece;
    private GameManager gameManager;

    void Awake()
    {
        tileBoard = new Tile[BoardSize, BoardSize];
        pieceBoard = new Piece[BoardSize, BoardSize];
        BoardCreator boardCreator = GetComponent<BoardCreator>();
        boardCreator.InitializeTiles();
        PieceCreator pieceCreator = GetComponent<PieceCreator>();
        pieceCreator.InitializePieces(this);
        SetTileBoard();
        SetPieceBoard();
    }
    public void SetDependencies(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void SetTileBoard()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        Debug.Log(tiles[0].position);
        foreach (Tile tile in tiles)
        {
            tileBoard[tile.position.x, tile.position.y] = tile;
        }
    }
    void SetPieceBoard()
    {
        Piece[] pieces = FindObjectsOfType<Piece>();
        foreach (Piece piece in pieces)
        {
            pieceBoard[piece.currentPosition.x, piece.currentPosition.y] = piece;
        }
    }

    public List<Piece> AllPieces()
    {
        List<Piece> pieces = new List<Piece>();
        foreach (Piece piece in pieceBoard)
        {
            if (piece != null)
            {
                pieces.Add(piece);
            }
        }
        return pieces;
    }

    public bool HasPiece(Piece piece)
    {
        return pieceBoard[piece.currentPosition.x, piece.currentPosition.y] != null;
    }

    public void selectPiece(Piece piece)
    {
        selectedPiece = piece;
    }

    public void deselectPiece()
    {
        selectedPiece = null;
    }

    public void OnSquareSelected(Vector3 inputPosition)
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(inputPosition.x), Mathf.RoundToInt(inputPosition.z));
        Piece piece = GetPieceOnSquare(pos);
        if (selectedPiece)
        {
            if (piece != null && selectedPiece == piece)
            {
                deselectPiece();
            }
            else if (piece != null && selectedPiece != piece && gameManager.IsTeamTurn(piece.team))
            {
                selectPiece(piece);
            }
            else if (selectedPiece.CanMoveTo(pos))
            {
                OnSelectedPieceMoved(pos, selectedPiece);
            }
        }
        else
        {
            if (piece != null && gameManager.IsTeamTurn(piece.team))
            {
                Debug.Log(piece);
                selectPiece(piece);
            }
        }

    }

    private void OnSelectedPieceMoved(Vector2Int pos, Piece piece)
    {
        UpdateBoardOnPieceMove(pos, piece.currentPosition, piece, null);
        selectedPiece.MovePiece(pos);
        deselectPiece();
        EndTurn();
    }

    private void EndTurn()
    {
        gameManager.EndTurn();
    }

    private void UpdateBoardOnPieceMove(Vector2Int newPos, Vector2Int oldPos, Piece newPiece, Piece oldPiece)
    {
        pieceBoard[oldPos.x, oldPos.y] = oldPiece;
        pieceBoard[newPos.x, newPos.y] = newPiece;
    }

    public Piece GetPieceOnSquare(Vector2Int coords)
    {
        if (IsWithinBounds(coords.x, coords.y))
        {
            return pieceBoard[coords.x, coords.y];
        }
        return null;
    }

    protected bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
    }

    public bool IsTakablePiece(Piece currentPiece, Piece takable)
    {
        return currentPiece.team != takable.team;
    }
}
