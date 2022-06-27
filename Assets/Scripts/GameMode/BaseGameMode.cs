using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameMode : MonoBehaviour
{
    public static bool gameEnd { get; private set; }

    public static UnityAction OnScoreChange;
    public static UnityAction OnTurnChange;
    public static UnityAction OnEndGame;

    private void Start()
    {
        gameEnd = false;

        GameboardManager.OnPointScore += PointScoreEventHandler;
        PlayerDrag.OnPlayerPlaceTile += PlaceTileEventHandler;
        GameboardManager.OnBoardFull += BoardFullEventHandler;
    }

    protected abstract void PointScoreEventHandler();
    protected abstract void PlaceTileEventHandler();

    protected virtual void BoardFullEventHandler()
    {
        gameEnd = true;
        OnEndGame();
    }
}
