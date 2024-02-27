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
    public bool hasMoved {get; private set;}
    public List<Vector2Int> availableMoves;
    public abstract List<Vector2Int> SelectAvailableSquares();
    Chessboard board;

    void Awake(){
        hasMoved = false;
        availableMoves = new List<Vector2Int>();
    }
    

    public bool IsFromSameTeam(Piece piece){
        return team == piece.team;
    }

    public bool CanMoveTo(Vector2Int coords){
        return availableMoves.Contains(coords);
    }

    public void SetData(string team,Vector2Int position,Chessboard board){
        this.team = team;
        currentPosition = position;
        this.board = board;
    }
    public void MovePiece(Vector2Int newPostition){
        currentPosition = newPostition;
        transform.position = new Vector3(newPostition.x,transform.position.y,newPostition.y);
    }

}
