using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;


public class GameboardManager : MonoBehaviour
{
    public Tilemap gameboard { get; private set; }

    private Dictionary<Vector3Int, bool> tileActive = new();
    private List<Vector3> serchPoints = new();

    private const float SEARCHRADIUS = 0.5f;
    private const int MINPOINTSTOSCORE = 4;

    public static UnityAction OnPointScore;

    [SerializeField] private LayerMask overlapMask;

    private void Start()
    {
        gameboard = GameObject.FindWithTag("Gameboard").GetComponent<Tilemap>();
        gameboard.CompressBounds();
        AddTiles();
    }

    /// <summary>
    /// Adds all used tiles to dictinary.
    /// </summary>
    private void AddTiles()
    {
        foreach(Vector3Int pos in gameboard.cellBounds.allPositionsWithin)
        {
            if(gameboard.HasTile(pos))
            {
                tileActive.Add(pos, false);
            }
        }
    }
    private bool IsOnBoard(Vector3Int pos)
    {
        return gameboard.HasTile(pos);
    }

    private bool CanPlaceTile(Vector3 pos)
    {
        Vector3Int cellPos = gameboard.WorldToCell(pos);
        if(IsOnBoard(cellPos))
        {
            if(!tileActive[cellPos])
            {
                return true;
            }
            
        }
        return false;
    }

    public bool TryPlaceTile(Vector3 pos)
    {
        if(CanPlaceTile(pos))
        {
            PlaceTile(pos);
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 SnapToGrid(Vector3 pos)
    {
        return gameboard.GetCellCenterWorld(gameboard.WorldToCell(pos));
    }

    private void PlaceTile(Vector3 pos)
    {
        tileActive[gameboard.WorldToCell(pos)] = true;
        serchPoints.Add(gameboard.WorldToCell(pos));
        SearchPoints();
    }

    private void SearchPoints()
    {
        List<Vector3> removeList = new();
        foreach(Vector3 pos in serchPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), SEARCHRADIUS, overlapMask);
            if(colliders.Length >= MINPOINTSTOSCORE)
            {
                if(SymbolsMatch(colliders))
                {
                    //OnPointScore();
                    removeList.Add(pos);

                    Debug.Log("point score");
                }
            }   
        }
        serchPoints.RemoveAll(i => removeList.Contains(i));
    }

    public bool SymbolsMatch(Collider2D[] colliders)
    {
        E_Symbol[] symbols = new E_Symbol[colliders.Length];
        for(int i = 0; i < symbols.Length; i++)
        {
            symbols[i] = colliders[i].gameObject.GetComponent<Symbol>().symbol;

            if(i > 0)
            {
                if(symbols[i] != symbols[i - 1])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
