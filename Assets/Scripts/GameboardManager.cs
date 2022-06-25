using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameboardManager : MonoBehaviour
{
    public Tilemap gameboard { get; private set; }
    private Dictionary<Vector3Int, bool> tileActive = new();
    private List<Vector3> serchPoints = new();

    private void Start()
    {
        gameboard = GameObject.FindWithTag("Gameboard").GetComponent<Tilemap>();
        gameboard.CompressBounds();
        AddTiles();

        PlayerDrag.OnPlaceTile += PlaceTileHandler;
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

    public bool CanPlaceTile(Vector3 pos)
    {
        Vector3Int cellPos = gameboard.WorldToCell(pos);
        if(IsOnBoard(cellPos))
        {
            if(!tileActive[cellPos])
            {
                tileActive[cellPos] = true;
                return true;
            }
            
        }

        return false;
    }

    public Vector3 SnapToGrid(Vector3 pos)
    {
        return gameboard.GetCellCenterWorld(gameboard.WorldToCell(pos));
    }

    private void PlaceTileHandler(Vector3 pos)
    {
        serchPoints.Add(gameboard.WorldToCell(pos));

        SearchPoints();
    }

    private void SearchPoints()
    {
        foreach(Vector3 pos in serchPoints)
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.5f);

            if(colliders.Length < 4)
            {
                break;
            }

            E_Symbol[] symbols = new E_Symbol[4];
            for(int i = 0; i < 4; i++)
            {
                symbols[i] = colliders[i].gameObject.GetComponent<Symbol>().symbol;

                if(i > 0)
                {
                    if(symbols[i] != symbols[i - 1])
                    {
                        break;
                    }
                }
            }

            Debug.Log("Score Point");
            serchPoints.Remove(pos);
            return;
        }
    }
}
