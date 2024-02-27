using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Chessboard : MonoBehaviour
{
    public Tile[,] tileBoard {get;set;}
    public Piece[,] pieceBoard {get;set;}
    public const int BoardSize = 8;
    private Piece selectedPiece;
    private GameManager gameManager;
    void Awake(){
        tileBoard = new Tile[BoardSize,BoardSize];
        pieceBoard = new Piece[BoardSize,BoardSize];
        BoardCreator boardCreator = GetComponent<BoardCreator>();
        boardCreator.InitializeTiles();
        PieceCreator pieceCreator = GetComponent<PieceCreator>();
        pieceCreator.InitializePieces(this);
        SetTileBoard();
        SetPieceBoard();
    }
    public void SetDependencies(GameManager gameManager){
        this.gameManager = gameManager;
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

    public List<Piece> AllPieces(){
        List<Piece> pieces = new List<Piece>();
        foreach(Piece piece in pieceBoard){
            if(piece !=null){
                pieces.Add(piece);
            }
        }
        return pieces;
    }

    public bool HasPiece(Piece piece){
        return pieceBoard[piece.currentPosition.x,piece.currentPosition.y] != null;
    }

    public void selectPiece(Piece piece){
        selectedPiece = piece;
    }

    public void deselectPiece(){
        selectedPiece = null;
    }
}
