using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// stores all needed resources for conveninet access.
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public SO_Symbol diamond,circle,square,triangle;
    public GameObject symbol;

    public static ResourceManager Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public SO_Symbol GetSymbolData(E_Symbol symbol)
    {
        switch(symbol)
        {
            case E_Symbol.diamond:
                return diamond;
            case E_Symbol.circle:
                return circle;
            case E_Symbol.square:
                return square;
            case E_Symbol.triangle:
                return triangle;
        }
        return null;
    }
}
