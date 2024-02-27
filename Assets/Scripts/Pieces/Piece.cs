using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string team;
    public Vector2Int currentPosition {get;set;}
    public bool hasMoved {get; private set;}
    public List<Vector2Int> availableMoves;
    public abstract List<Vector2Int> SelectAvailableSquares();

    void Awake(){
        hasMoved = false;
    }
    void Start()
    {
        
    }

    public void SetData(string team,Vector2Int position){
        this.team = team;
        currentPosition = position;
    }

}
