using UnityEngine;
using TMPro;

public class VsLocalUIManager : BaseUIManager
{
    [SerializeField] private TextMeshProUGUI player1Turn, player1Score, player2Turn, player2Score;
    [SerializeField] private TextMeshProUGUI finalWinner, player1FinalScore, player2FinalScore;

    private const string SCORE = "SCORE ";
    private const string TURN = "TURN";
    private const string PLAYER1NAME = "PLAYER 1 ";
    private const string PLAYER2NAME = "PLAYER 2 ";
    private const string WON = "WON";

    protected override void PointScoreEventHandler()
    {
        if(VsLocalGameMode.isPlayer1Turn)
        {
            player1Score.text = SCORE + VsLocalGameMode.player1Score;
        }
        else
        {
            player2Score.text = SCORE + VsLocalGameMode.player2Score;
        }
    }

    protected override void TurnChangeEventHandler()
    {
        if(VsLocalGameMode.isPlayer1Turn)
        {
            SetPlayer1Turn();
        }
        else
        {
            SetPlayer2Turn();
        }
    }

    private void SetPlayer1Turn()
    {
        player2Turn.fontStyle = FontStyles.Normal;
        player2Turn.text = PLAYER2NAME;

        player1Turn.fontStyle = FontStyles.Bold;
        player1Turn.text = PLAYER1NAME + TURN;
    }

    private void SetPlayer2Turn()
    {
        player1Turn.fontStyle = FontStyles.Normal;
        player1Turn.text = PLAYER1NAME;

        player2Turn.fontStyle = FontStyles.Bold;
        player2Turn.text = PLAYER2NAME + TURN;
    }

    protected override void EndGameEventHandler()
    {
        EndGameUI.SetActive(true);

        if(VsLocalGameMode.finalWinner == 1)
        {
            finalWinner.text = PLAYER1NAME + WON;
        }
        else if(VsLocalGameMode.finalWinner == 2)
        {
            finalWinner.text = PLAYER2NAME + WON;
        }

        player1FinalScore.text = PLAYER1NAME + SCORE + VsLocalGameMode.player1Score;
        player2FinalScore.text = PLAYER2NAME + SCORE + VsLocalGameMode.player2Score;
    }
}
