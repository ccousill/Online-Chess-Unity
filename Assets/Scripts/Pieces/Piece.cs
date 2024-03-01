using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string team;
    public Player player {get;set;}
    public Vector2Int currentPosition {get;set;}
    public Vector2Int previousPosition {get;set;}
    public bool hasMoved {get; private set;}
    public int pieceValue {get; private set;}
    public List<Vector2Int> availableMoves;
    public abstract List<Vector2Int> SelectAvailableSquares();
    public Chessboard board;

    void Awake(){
        hasMoved = false;
        previousPosition = new Vector2Int();
        availableMoves = new List<Vector2Int>();
    }
    

    public bool IsFromSameTeam(Piece piece){
        return team == piece.team;
    }

    public bool CanMoveTo(Vector2Int coords){
        return availableMoves.Contains(coords);
    }

    public void SetData(string team,Vector2Int position,Chessboard board, int pieceValue){
        this.team = team;
        this.pieceValue = pieceValue;
        currentPosition = position;
        this.board = board;
    }
    public void MovePiece(Vector2Int newPostition){
        previousPosition = currentPosition;
        currentPosition = newPostition;
        hasMoved = true;
        transform.position = new Vector3(newPostition.x,transform.position.y,newPostition.y);
    }

    protected bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < Chessboard.BoardSize && y >= 0 && y < Chessboard.BoardSize;
    }

}
