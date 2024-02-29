using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int BoardSize = 8;
    Chessboard board;
    private Player whitePlayer;
    private Player blackPlayer;
    private Player activePlayer;
    void Start()
    {
        board = FindObjectOfType<Chessboard>();
        InitializePlayers();
        Debug.Log(board.pieceBoard[0,0].player.PlayerColor);
        StartNewGame();
    }

    private void InitializePlayers(){
        Player white = new Player("White",board);
        Player black = new Player("Black",board);
        whitePlayer = white;
        blackPlayer = black;
        foreach(Piece piece in board.AllPieces()){
            if(piece.team == "White"){
                piece.player = white;
                whitePlayer.AddActivePiece(piece);
            }else{
                piece.player = black;
                blackPlayer.AddActivePiece(piece);
            } 
        }
    }

    void StartNewGame(){
        board.SetDependencies(this);
        activePlayer = whitePlayer;
        GenerateAllMovesOfPlayer(activePlayer);
    }

    public void EndTurn(){
        GenerateAllMovesOfPlayer(activePlayer);
        GenerateAllMovesOfPlayer(getOtherPlayer(activePlayer));
        ChangeTeam();
        Debug.Log(activePlayer.PlayerColor);
    }
 
    private void GenerateAllMovesOfPlayer(Player player){
        player.GenerateAllPossibleMoves();
    }

    private Player getOtherPlayer(Player player){
        return player == whitePlayer ?  blackPlayer : whitePlayer; 
    }

    private void ChangeTeam(){
        activePlayer = activePlayer == whitePlayer ? blackPlayer : whitePlayer;
    }

    public bool IsTeamTurn(string team){
        return activePlayer.PlayerColor == team;
    }
}
