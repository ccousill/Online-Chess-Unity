using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Chessboard : MonoBehaviour
{
    public Tile[,] tileBoard {get;set;}
    public Piece[,] pieceBoard {get;set;}
    public const int BoardSize = 8;
    void Awake(){
        tileBoard = new Tile[BoardSize,BoardSize];
        pieceBoard = new Piece[BoardSize,BoardSize];
        BoardCreator boardCreator = GetComponent<BoardCreator>();
        boardCreator.InitializeTiles();
        PieceCreator pieceCreator = GetComponent<PieceCreator>();
        pieceCreator.InitializePieces();
        SetTileBoard();
        SetPieceBoard();
        Debug.Log(pieceBoard[4,0]);
    }

    void SetTileBoard(){
        Tile[] tiles = FindObjectsOfType<Tile>();
        Debug.Log(tiles[0].position);
        foreach(Tile tile in tiles){
            tileBoard[tile.position.x,tile.position.y] = tile;
        }
    }

    void SetPieceBoard(){
        Piece[] pieces = FindObjectsOfType<Piece>();
        foreach(Piece piece in pieces){
             pieceBoard[piece.currentPosition.x,piece.currentPosition.y] = piece;
        }
    }
}
