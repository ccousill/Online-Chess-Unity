using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{

    [SerializeField] GameObject WhiteTile;
    [SerializeField] GameObject BlackTile;
    const int boardSize = 8;
    GameObject[,] tiles = new GameObject[boardSize,boardSize];
    void Awake()
    {

    }
    public void InitializeTiles(){
        bool black = true;
        GameObject TilesObject = new GameObject("Tiles");
        TilesObject.transform.parent = transform;
        for(int x = 0;x<boardSize;x++){
            for(int y = 0;y<boardSize;y++){
                if(black){
                    tiles[x,y] = Instantiate(BlackTile, new Vector3(x,0,y),Quaternion.identity);
                    
                }else{
                    tiles[x,y] = Instantiate(WhiteTile, new Vector3(x,0,y),Quaternion.identity);
                }
                tiles[x,y].transform.parent = TilesObject.transform;
                tiles[x,y].layer = LayerMask.NameToLayer("Tile");
                Tile newTile = tiles[x,y].GetComponent<Tile>();
                newTile.setData(black);
                if(y!=boardSize-1){
                    black = !black; 
                }
                
            }
        }
    }
}
