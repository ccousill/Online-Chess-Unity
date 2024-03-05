using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int BoardSize = 8;
    Chessboard board;
    private Player whitePlayer;
    private Player blackPlayer;
    public Player activePlayer {get;set;}
    void Start()
    {
        board = FindObjectOfType<Chessboard>();
        StartNewGame();
    }

    private void InitializePlayers(){
        Player white = new Player("White",board);
        Player black = new Player("Black",board);
        whitePlayer = white;
        blackPlayer = black;
        whitePlayer.ClearActivePieces();
        blackPlayer.ClearActivePieces();
        foreach(Piece piece in board.AllPieces()){
            if(piece.team == "White"){
                piece.player = white;
                whitePlayer.AddActivePiece(piece);
            }else{
                piece.player = black;
                blackPlayer.AddActivePiece(piece);
            } 
        }
    }

    public void StartNewGame(){
        InitializePlayers();
        board.SetDependencies(this);
        activePlayer = whitePlayer;
        GenerateAllMovesOfPlayer(activePlayer);
    }

    public void EndTurn(){
        GenerateAllMovesOfPlayer(activePlayer);
        GenerateAllMovesOfPlayer(getOtherPlayer(activePlayer));
        ChangeTeam();
    }
 
    private void GenerateAllMovesOfPlayer(Player player){
        player.GenerateAllPossibleMoves();
    }

    private Player getOtherPlayer(Player player){
        return player == whitePlayer ?  blackPlayer : whitePlayer; 
    }

    private void ChangeTeam(){
        activePlayer = activePlayer == whitePlayer ? blackPlayer : whitePlayer;
    }

    public bool IsTeamTurn(string team){
        return activePlayer.PlayerColor == team;
    }

   

    public void RemovePieceFromPlayer(Piece piece){
        activePlayer.RemoveActivePiece(piece);
    }

    public void RemovePieceFromOtherPlayer(Piece piece){
        getOtherPlayer(activePlayer).RemoveActivePiece(piece);
    }

    public void AddPieceToPlayer(Piece piece){
        activePlayer.AddActivePiece(piece);
    }

    public void AddPieceToOtherPlayer(Piece piece){
        getOtherPlayer(activePlayer).AddActivePiece(piece);
    }

}
