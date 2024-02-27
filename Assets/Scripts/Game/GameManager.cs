using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int BoardSize = 8;
    Chessboard board;
    void Start()
    {
        board = FindObjectOfType<Chessboard>();
        Debug.Log(board);
        
    }

}
