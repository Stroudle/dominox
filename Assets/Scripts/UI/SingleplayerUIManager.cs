using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleplayerUIManager : BaseUIManager
{
    [SerializeField] private TextMeshProUGUI player1turn, player1score, player2turn, player2score;

    private const string SCORE = "SCORE ";
    private const string TURN = "TURN";
    private const string PLAYER1NAME = "PLAYER 1 ";
    private const string PLAYER2NAME = "PLAYER 2 ";

    protected override void PointScoreEventHandler(float value)
    {
        if(SingleplayerGameMode.player1Turn)
        {
            player1score.text = SCORE + value;
        }
        else
        {
            player2score.text = SCORE + value;
        }
    }

    protected override void TurnChangeEventHandler()
    {
        if(SingleplayerGameMode.player1Turn)
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
        player2turn.fontStyle = FontStyles.Normal;
        player2turn.text = PLAYER2NAME;

        player1turn.fontStyle = FontStyles.Bold;
        player1turn.text = PLAYER1NAME + TURN;
    }

    private void SetPlayer2Turn()
    {
        player1turn.fontStyle = FontStyles.Normal;
        player1turn.text = PLAYER1NAME;

        player2turn.fontStyle = FontStyles.Bold;
        player2turn.text = PLAYER2NAME + TURN;
    }
}
