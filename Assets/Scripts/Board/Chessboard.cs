using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;

public class Chessboard : MonoBehaviour
{
    [SerializeField] GameObject whiteQueenPrefab;
    [SerializeField] GameObject blackQueenPrefab;
    public Tile[,] tileBoard { get; set; }
    public Piece[,] pieceBoard { get; set; }
    public Piece enPassantablePiece { get; set; }
    public const int BoardSize = 8;

    private Piece selectedPiece;
    private GameManager gameManager;
    private SquareSelectorCreator squareSelector;
    private PieceCreator pieceCreator;
    private BoardCreator boardCreator;


    void Awake()
    {
        tileBoard = new Tile[BoardSize, BoardSize];
        pieceBoard = new Piece[BoardSize, BoardSize];
        enPassantablePiece = null;
        boardCreator = GetComponent<BoardCreator>();
        boardCreator.InitializeTiles();
        pieceCreator = GetComponent<PieceCreator>();
        squareSelector = GetComponent<SquareSelectorCreator>();
        pieceCreator.InitializePieces(this);
        SetTileBoard();
        SetPieceBoard();
    }
    public void SetDependencies(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void SetTileBoard()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        Debug.Log(tiles[0].position);
        foreach (Tile tile in tiles)
        {
            tileBoard[tile.position.x, tile.position.y] = tile;
        }
    }
    void SetPieceBoard()
    {
        Piece[] pieces = FindObjectsOfType<Piece>();
        foreach (Piece piece in pieces)
        {
            pieceBoard[piece.currentPosition.x, piece.currentPosition.y] = piece;
        }
    }

    public List<Piece> AllPieces()
    {
        List<Piece> pieces = new List<Piece>();
        foreach (Piece piece in pieceBoard)
        {
            if (piece != null)
            {
                pieces.Add(piece);
            }
        }
        return pieces;
    }

    public bool HasPiece(Piece piece)
    {
        return pieceBoard[piece.currentPosition.x, piece.currentPosition.y] != null;
    }

    public void selectPiece(Piece piece)
    {
        selectedPiece = piece;
        List<Vector2Int> selection = selectedPiece.availableMoves;
        ShowSelectionSquares(selection);
    }

    private void ShowSelectionSquares(List<Vector2Int> selection)
    {
        Dictionary<Vector3, bool> squaresData = new Dictionary<Vector3, bool>();
        for (int i = 0; i < selection.Count; i++)
        {
            Vector3 position = new Vector3(selection[i].x, .2f, selection[i].y);
            bool isSquareFree = GetPieceOnSquare(selection[i]) == null;
            if (enPassantablePiece && isSquareFree)
            {
                isSquareFree = !CheckEnpassant(selection[i]);
            }
            squaresData.Add(position, isSquareFree);
        }
        squareSelector.ShowSelection(squaresData);
    }

    public void DeselectPiece()
    {
        selectedPiece = null;
        squareSelector.ClearSelection();
    }

    public void OnSquareSelected(Vector3 inputPosition)
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(inputPosition.x), Mathf.RoundToInt(inputPosition.z));
        Piece piece = GetPieceOnSquare(pos);
        if (selectedPiece)
        {
            if (piece != null && selectedPiece == piece)
            {
                DeselectPiece();
            }
            else if (piece != null && selectedPiece != piece && gameManager.IsTeamTurn(piece.team))
            {
                selectPiece(piece);
            }
            else if (selectedPiece.CanMoveTo(pos))
            {
                OnSelectedPieceMoved(pos, selectedPiece, piece);
            }
        }
        else
        {
            if (piece != null && gameManager.IsTeamTurn(piece.team))
            {
                Debug.Log(piece.currentPosition);
                selectPiece(piece);
            }
        }

    }

    private void OnSelectedPieceMoved(Vector2Int pos, Piece piece, Piece tookPiece)
    {
        if (enPassantablePiece && CheckEnpassant(pos))
        {
            tookPiece = enPassantablePiece;
        }
        enPassantablePiece = null;
        UpdateBoardOnPieceMove(pos, piece.currentPosition, piece, tookPiece);
        selectedPiece.MovePiece(pos);
        if (IsTakablePiece(piece, tookPiece))
        {
            Destroy(tookPiece.gameObject);
        }
        if (piece is King && !piece.hasMoved)
        {
            CheckCastle(piece, pos);
        }
        
        if (piece is Pawn)
        {
            Pawn pawn = (Pawn)piece;
            Debug.Log(pawn.HasReachedEnd());
            if (pawn.HasReachedEnd())
            {
                Debug.Log("promotion");
                PromotePawn(piece);
            }
            if (Mathf.Abs(pawn.currentPosition.y - pos.y) == 2)
            {
                enPassantablePiece = piece;
            }
        }
        
        DeselectPiece();
        EndTurn();
    }

    private void PromotePawn(Piece piece)
    {
        Piece newPiece;
        if (piece.team == "White")
        {
            newPiece = pieceCreator.InitializePiece(this,piece,"Queen White");

        }else{
            newPiece = pieceCreator.InitializePiece(this,piece,"Queen Black");
        }
        
        Destroy(piece.gameObject);
        gameManager.activePlayer.AddActivePiece(newPiece);
        selectedPiece = newPiece;
    }

    private void CheckCastle(Piece piece, Vector2Int position)
    {
        if (position.x == piece.currentPosition.x - 2)
        {
            //handle left castle
            Piece leftRook = GetPieceOnSquare(new Vector2Int(0, piece.currentPosition.y));
            Vector2Int newPos = new Vector2Int(position.x + 1, position.y);
            UpdateBoardOnPieceMove(newPos, leftRook.currentPosition, leftRook, null);
            leftRook.MovePiece(newPos);
        }
        else if (position.x == piece.currentPosition.x + 2)
        {
            //handle right castle
            Piece rightRook = GetPieceOnSquare(new Vector2Int(7, piece.currentPosition.y));
            Vector2Int newPos = new Vector2Int(position.x - 1, position.y);
            UpdateBoardOnPieceMove(newPos, rightRook.currentPosition, rightRook, null);
            rightRook.MovePiece(newPos);
        }
    }

    private bool CheckEnpassant(Vector2Int pos)
    {
        if (enPassantablePiece.team == "White")
        {
            if (enPassantablePiece.currentPosition.y - 1 == pos.y && enPassantablePiece.currentPosition.x == pos.x)
            {
                return true;
            }
        }
        else
        {
            if (enPassantablePiece.currentPosition.y + 1 == pos.y && enPassantablePiece.currentPosition.x == pos.x)
            {
                return true;
            }
        }
        return false;
    }

    private void EndTurn()
    {
        gameManager.EndTurn();
    }

    private void UpdateBoardOnPieceMove(Vector2Int newPos, Vector2Int oldPos, Piece newPiece, Piece oldPiece)
    {
        pieceBoard[oldPos.x, oldPos.y] = oldPiece;
        pieceBoard[newPos.x, newPos.y] = newPiece;
    }

    public Piece GetPieceOnSquare(Vector2Int coords)
    {
        if (IsWithinBounds(coords.x, coords.y))
        {
            return pieceBoard[coords.x, coords.y];
        }
        return null;
    }

    protected bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
    }

    public bool IsTakablePiece(Piece currentPiece, Piece takable)
    {
        if (takable == null)
        {
            return false;
        }
        return currentPiece.team != takable.team;
    }
}
