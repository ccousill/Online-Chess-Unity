using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public String color {get;set;}
    public Vector2Int position {get;set;}
    void Awake(){
    }

    public void setData(bool isBlack){
        if(isBlack){
            color = "Black";
        }
        else{
            color = "White";
        }
        position = new Vector2Int(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.z));
    }

}
