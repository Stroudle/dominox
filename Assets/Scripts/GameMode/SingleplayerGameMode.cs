using UnityEngine;
using UnityEngine.Events;

public class SingleplayerGameMode : BaseGameMode
{
    public static bool player1Turn { get; private set; }

    private const float SCOREINCREASE = 100f;
    private float player1Score, player2Score;

    private void Awake()
    {
        player1Turn = true;
        player1Score = player2Score = 0;
    }

    protected override void PointScoreEventHandler()
    {
        if(player1Turn)
        {
            player1Score += SCOREINCREASE;
            OnScoreChange(player1Score);
        }
        else
        {
            player2Score += SCOREINCREASE;
            OnScoreChange(player2Score);
        }
    }

    protected override void PlaceTileEventHandler()
    {
        player1Turn = !player1Turn;
        OnTurnChange();
    }
}
