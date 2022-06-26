using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUIManager : MonoBehaviour
{
    [SerializeField]protected GameObject EndGameUI;

    private void Start()
    {
        BaseGameMode.OnScoreChange += PointScoreEventHandler;
        BaseGameMode.OnTurnChange += TurnChangeEventHandler;
        BaseGameMode.OnEndGame += EndGameEventHandler;
    }

    protected abstract void PointScoreEventHandler();
    protected abstract void TurnChangeEventHandler();
    protected abstract void EndGameEventHandler();
}
