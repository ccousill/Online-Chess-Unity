using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PieceCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] piecePrefabs; 
    
    Dictionary<string,GameObject> dictionary = new Dictionary<string, GameObject>();
    private string[,] pieceSetup2 = {
    {"Rook White","Pawn White",null,null,null,null,"Pawn Black","Rook Black"},
    {"Knight White","Pawn White",null,null,null,null,"Pawn Black","Knight Black"},
    {"Bishop White","Pawn White",null,null,null,null,"Pawn Black","Bishop Black"},
    {"Queen White","Pawn White",null,null,null,null,"Pawn Black","Queen Black"},
    {"King White","Pawn White",null,null,null,null,"Pawn Black","King Black"},
    {"Bishop White","Pawn White",null,null,null,null,"Pawn Black","Bishop Black"},
    {"Knight White","Pawn White",null,null,null,null,"Pawn Black","Knight Black"},
    {"Rook White","Pawn White",null,null,null,null,"Pawn Black","Rook Black"}
    };

    private string[,] pieceSetup1 = {
    {"Rook White","Pawn White",null,null,null,null,"Pawn Black","Rook Black"},
    {null,"Pawn White",null,null,null,null,"Pawn Black","Knight Black"},
    {null,"Pawn White",null,null,null,null,"Pawn Black","Bishop Black"},
    {null,"Pawn White",null,null,null,null,"Pawn Black","Queen Black"},
    {"King White","Pawn White",null,null,null,null,"Pawn Black","King Black"},
    {null,"Pawn White",null,null,null,null,"Pawn Black","Bishop Black"},
    {null,"Pawn White",null,null,null,null,"Pawn Black","Knight Black"},
    {"Rook White","Pawn White",null,null,null,null,"Pawn Black","Rook Black"}
    };

    private string[,] pieceSetup = {
    {"Rook White","Pawn White",null,null,null,null,"Pawn Black","Rook Black"},
    {"Knight White","Pawn White",null,null,null,null,"Pawn Black","Knight Black"},
    {"Bishop White","Pawn White",null,null,null,null,"Pawn Black","Bishop Black"},
    {"Queen White","Pawn White",null,null,null,null,"Pawn Black","Queen Black"},
    {"King White","Pawn White",null,null,null,null,"Pawn Black","King Black"},
    {"Bishop White","Pawn White",null,null,null,null,"Pawn White",null},
    {"Knight White","Pawn White",null,null,null,null,"Pawn Black","Knight Black"},
    {"Rook White","Pawn White",null,null,null,null,"Pawn Black","Rook Black"}
    };

    
    void Awake(){
        Debug.Log("awaking");
        foreach(GameObject piecePrefab in piecePrefabs){
            dictionary[piecePrefab.name] = piecePrefab;
        }
    }

    public void InitializePieces(Chessboard board){
        GameObject PiecesObject = new GameObject("Pieces");
        PiecesObject.transform.parent = transform;
        for(int x = 0;x<8;x++){
            for(int y = 0;y <8;y++){
                if(pieceSetup[x,y]!=null){
                    string color = pieceSetup[x,y].Split(' ')[1];
                    GameObject pieceObject = CreatePiece(pieceSetup[x,y],new Vector2Int(x,y));
                    pieceObject.transform.parent = PiecesObject.transform;
                    pieceObject.name = pieceSetup[x,y];
                    Piece piece = pieceObject.GetComponent<Piece>();
                    piece.SetData(color,new Vector2Int(x,y),board);
                }
            }
        }
    }

    public Piece InitializePiece(Chessboard board,Piece previousPiece,string prefabName){
        Transform PiecesObject = transform.Find("Pieces");
        Vector2Int prevPos = previousPiece.currentPosition;
        string color = prefabName.Split(' ')[1];
        GameObject pieceObject = CreatePiece(prefabName,prevPos);
        pieceObject.transform.parent = PiecesObject.transform;
        pieceObject.name = prefabName;
        Piece piece = pieceObject.GetComponent<Piece>();
        piece.SetData(color,prevPos,board);
        board.pieceBoard[prevPos.x,prevPos.y] = piece;
        return piece;
    }

    private GameObject CreatePiece(string pieceName,Vector2Int position){
        GameObject prefab = dictionary[pieceName];
        if(prefab){
            GameObject newPiece = Instantiate(prefab,new Vector3(position.x,.13f,position.y),Quaternion.identity);
            return newPiece;
        }
        return null;
    }











}

    