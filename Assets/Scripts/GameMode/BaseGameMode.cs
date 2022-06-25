using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameMode : MonoBehaviour
{
    public static UnityAction<float> OnScoreChange;
    public static UnityAction OnTurnChange;

    private void Start()
    {
        GameboardManager.OnPointScore += PointScoreEventHandler;
        PlayerDrag.OnPlaceTile += PlaceTileEventHandler;
    }

    protected abstract void PointScoreEventHandler();
    protected abstract void PlaceTileEventHandler();
}
