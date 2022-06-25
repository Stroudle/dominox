using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUIManager : MonoBehaviour
{
    private void Start()
    {
        BaseGameMode.OnScoreChange += PointScoreEventHandler;
        BaseGameMode.OnTurnChange += TurnChangeEventHandler;
    }

    protected abstract void PointScoreEventHandler(float value);
    protected abstract void TurnChangeEventHandler();
}
