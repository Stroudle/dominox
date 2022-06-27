using UnityEngine;

/// <summary>
/// Stores all needed resources for convenient access.
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    public GameboardManager boardManager { get; private set; }

    [SerializeField] private SO_Symbol circleOpen, circle;
    [SerializeField] private GameObject playerTile, aiTile;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;


        boardManager = GameObject.FindWithTag("GameboardManager").GetComponent<GameboardManager>();
    }

    public SO_Symbol GetSymbolData(E_Symbol symbol)
    {
        switch(symbol)
        {
            case E_Symbol.circleOpen:
                return circleOpen;
            case E_Symbol.circle:
                return circle;
        }
        return null;
    }

    public GameObject GetPlayerTile()
    {
        return playerTile;
    }

    public GameObject GetAITile()
    {
        return aiTile;
    }
}
