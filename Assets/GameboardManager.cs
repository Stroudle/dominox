using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameboardManager : MonoBehaviour
{
    public Tilemap gameboard { get; private set; }

    private Dictionary<Vector3Int, bool> tiles = new();

    private void Start()
    {
        gameboard = GameObject.FindWithTag("Gameboard").GetComponent<Tilemap>();
        AddTiles();
    }

    /// <summary>
    /// Adds all used tiles to dictinary.
    /// </summary>
    private void AddTiles()
    {
        gameboard.CompressBounds();
        foreach(Vector3Int pos in gameboard.cellBounds.allPositionsWithin)
        {
            if(gameboard.HasTile(pos))
            {
                tiles.Add(pos, false);
            }
        }
    }

    public bool CanPlaceTile(Vector3 pos)
    {
        Vector3Int cellPos = gameboard.WorldToCell(pos);
        if(IsOnBoard(cellPos))
        {
            if(!tiles[cellPos])
            {
                tiles[cellPos] = true;
                return true;
            }
            
        }

        return false;
    }

    private bool IsOnBoard(Vector3Int pos)
    {
        return gameboard.HasTile(pos);
    }
}
