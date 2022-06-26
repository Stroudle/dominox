using System;
using UnityEngine;
using UnityEngine.Events;

public class VsLocalGameMode : BaseGameMode
{
    public static bool isPlayer1Turn { get; private set; }
    public static float player1Score { get; private set; }
    public static float player2Score { get; private set; }

    public static int finalWinner { get; private set; }

    private const float SCOREINCREASE = 100f;

    private void Awake()
    {
        isPlayer1Turn = true;
        player1Score = player2Score = 0;
        finalWinner = 0;
    }

    protected override void PointScoreEventHandler()
    {
        if(isPlayer1Turn)
        {
            player1Score += SCOREINCREASE;
        }
        else
        {
            player2Score += SCOREINCREASE;
        }
        OnScoreChange();
    }

    protected override void PlaceTileEventHandler()
    {
        isPlayer1Turn = !isPlayer1Turn;
        OnTurnChange();
    }

    protected override void BoardFullEventHandler()
    {
        DecideWinner();
        base.BoardFullEventHandler();
    }

    private void DecideWinner()
    {
        if(player1Score > player2Score)
        {
            finalWinner = 1;
        }
        else
        {
            finalWinner = 2;
        }
    }
}
