using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class Player
{
    public string PlayerColor {get;set;}
    public List<Piece> CapturedPieces {get;}
     public List<Piece> activePieces {get;private set;}
    public Chessboard board {get;set;}
   

    public Player(string playerColor,Chessboard board){
        PlayerColor = playerColor;
        CapturedPieces = new List<Piece>();
        this.board = board;
        activePieces = new List<Piece>();

    }
    public void AddActivePiece(Piece piece){
        if(!activePieces.Contains(piece)){
            activePieces.Add(piece);
        }
    }

    public void RemovePiece(Piece piece){
        if(activePieces.Contains(piece)){
            activePieces.Remove(piece);
        }
    }

    public void GenerateAllPossibleMoves(){
        foreach(var piece in activePieces){
            if(board.HasPiece(piece)){
                piece.SelectAvailableSquares();
            }
        }
    }
    public void addCapturedPiece(Piece piece){
        CapturedPieces.Add(piece);
    }
}
