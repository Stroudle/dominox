using UnityEngine;

/// <summary>
/// Stores all needed resources for convenient access.
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    public GameboardManager boardManager { get; private set; }

    [SerializeField] private SO_Symbol diamond,circle,square,triangle;
    [SerializeField] private GameObject playerTile, tile;

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

    public GameObject GetPlayerTile()
    {
        return playerTile;
    }

    public GameObject GetTile()
    {
        return tile;
    }
}
