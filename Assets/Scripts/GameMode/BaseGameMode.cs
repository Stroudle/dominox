using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameMode : MonoBehaviour
{
    public static UnityAction OnScoreChange;
    public static UnityAction OnTurnChange;
    public static UnityAction OnEndGame;

    private void Start()
    {
        GameboardManager.OnPointScore += PointScoreEventHandler;
        PlayerDrag.OnPlaceTile += PlaceTileEventHandler;
        GameboardManager.OnBoardFull += BoardFullEventHandler;
    }

    protected abstract void PointScoreEventHandler();
    protected abstract void PlaceTileEventHandler();

    protected virtual void BoardFullEventHandler()
    {
        OnEndGame();
    }
}
