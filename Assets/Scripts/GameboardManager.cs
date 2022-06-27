using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class GameboardManager : MonoBehaviour
{
    public Tilemap gameboard { get; private set; }

    public Dictionary<Vector3Int, bool> tileFull { get; private set; }
    public List<Vector3Int> serchPositions { get; private set; }

    private const float SEARCHRADIUS = 0.5f;
    private const int MINPOINTSTOSCORE = 4;

    public static UnityAction OnPointScore;
    public static UnityAction OnBoardFull;

    [SerializeField] Vector2Int minGridSize;

    [SerializeField] private LayerMask overlapMask;

    private void Start()
    {
        gameboard = GameObject.FindWithTag("Gameboard").GetComponent<Tilemap>();

        tileFull = new Dictionary<Vector3Int, bool>();
        serchPositions = new List<Vector3Int>();

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
                tileFull.Add(pos, false);
            }
        }
    }

    public Vector3 SnapToGrid(Vector3 pos)
    {
        return gameboard.GetCellCenterWorld(gameboard.WorldToCell(pos));
    }

    private bool IsOnBoard(Vector3Int pos)
    {
        return gameboard.HasTile(pos);
    }

    public bool CanPlaceTile(Vector3Int pos)
    {
        if(IsOnBoard(pos))
        {
            if(!tileFull[pos])
            {
                return true;
            }
        }
        return false;
    }

    public bool TryPlaceTile(Vector3 pos)
    {
        Vector3Int cellPos = gameboard.WorldToCell(pos);
        if(CanPlaceTile(cellPos))
        {
            PlaceTile(cellPos);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PlaceTile(Vector3Int pos)
    {
        tileFull[pos] = true;
        if(pos.x > minGridSize.x && pos.y > minGridSize.y)
        {
            serchPositions.Add(pos);
        }
        SearchPositions();
    }

    private void SearchPositions()
    {
        List<Vector3Int> removeList = new List<Vector3Int>();
        E_Symbol unused = new E_Symbol();
        foreach(Vector3Int pos in serchPositions)
        {
            if(VerifyIsPositionScore(pos, MINPOINTSTOSCORE, ref unused))
            {
                //OnPointScore();
                removeList.Add(pos);
            }  
        }
        serchPositions.RemoveAll(i => removeList.Contains(i));
        VerifyBoadFull();
    }

    public bool VerifyIsPositionScore(Vector3Int pos, int minPoints, ref E_Symbol matchSymbol)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), SEARCHRADIUS, overlapMask);

        if(colliders.Length >= minPoints)
        {
            if(VerifySymbolsMatch(colliders, ref matchSymbol))
            {
                return true;
            }
        }
        return false;
    }

    private bool VerifySymbolsMatch(Collider2D[] colliders, ref E_Symbol matchSymbol)
    {
        E_Symbol[] symbols = new E_Symbol[colliders.Length];
        for(int i = 0; i < symbols.Length; i++)
        {
            symbols[i] = colliders[i].gameObject.GetComponent<BaseSymbol>().symbol;

            if(i > 0)
            {
                if(symbols[i] != symbols[i - 1])
                {
                    return false;
                }
            }
            matchSymbol = symbols[i];
        }
        return true;
    }

    private void VerifyBoadFull()
    {
        foreach(var i in tileFull)
        {
            if(!i.Value)
            {
                return;
            }
        }
        OnBoardFull();
    }
}
