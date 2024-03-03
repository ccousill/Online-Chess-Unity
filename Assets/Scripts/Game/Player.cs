using System.Collections.Generic;
public class Player
{
    public string PlayerColor {get;set;}
    public List<Piece> capturedPieces {get;}
    public List<Piece> activePieces {get;private set;}
    public Chessboard board {get;set;}

    public Player(string playerColor,Chessboard board){
        PlayerColor = playerColor;
        capturedPieces = new List<Piece>();
        this.board = board;
        activePieces = new List<Piece>();

    }
    public void AddActivePiece(Piece piece){
        if(!activePieces.Contains(piece)){
            activePieces.Add(piece);
        }
    }

    public void RemoveActivePiece(Piece piece){
        if(activePieces.Contains(piece)){
            activePieces.Remove(piece);
        }
    }

    public void GenerateAllPossibleMoves(){
        foreach(var piece in activePieces){
            if(board.HasPiece(piece)){
                piece.SelectAvailableSquares();
            }
        }
    }
    public void addCapturedPiece(Piece piece){
        capturedPieces.Add(piece);
    }

    public int totalPieceScore(){
        int score = 0;
        foreach(Piece piece in activePieces){
            score += piece.pieceValue;
        }
        return score;
    }
}
