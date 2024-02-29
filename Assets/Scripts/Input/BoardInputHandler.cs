using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInputHandler : MonoBehaviour, IInputHandler
{

    private Chessboard board;

    private void Awake(){
        board = GetComponent<Chessboard>();
    }
    public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
    {
        board.OnSquareSelected(inputPosition);
    }
}
