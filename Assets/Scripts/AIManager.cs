using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private List<Vector3Int> emptyTiles = new List<Vector3Int>();

    private GameboardManager boardManager;
    private ResourceManager resources;

    private const int POINTSTOSEARCH = 3;

    private void Awake()
    {
        resources = ResourceManager.Instance;
        boardManager = resources.boardManager;
    }

    private void Start()
    {
        PlayerDrag.OnPlayerPlaceTile += PlayerPlaceTileEventHandler;
    }

    private void PlayerPlaceTileEventHandler()
    {
        if(!BaseGameMode.gameEnd)
        {
            AIFindPositionToSpawn();
        }
    }

    private void AIFindPositionToSpawn()
    {
        emptyTiles.Clear();

        foreach(var i in boardManager.tileFull)
        {
            if(!i.Value)
            {
                emptyTiles.Add(i.Key);
            }
        }

        TryFindPositionToScore();
    }

    /// <summary>
    /// AI looks for position missing only a symbol to score and instantiates.
    /// </summary>
    private void TryFindPositionToScore()
    {
        E_Symbol matchSymbol = new E_Symbol();
        foreach(Vector3Int pos in emptyTiles)
        {
            if(boardManager.VerifyIsPositionScore(pos, POINTSTOSEARCH, ref matchSymbol))
            {
                AIPlaceTile(pos, matchSymbol);
                return;
            }
        }

        FindPositionWithNeighbour();
    }

    /// <summary>
    /// If no positions to score were found, AI instantiates at random position containing at least one neighbour.
    /// </summary>
    private void FindPositionWithNeighbour()
    {
        if(boardManager.serchPositions.Count == 0)
        {
            FindRandomPosition();
        }
        else
        {
            Vector3Int pos = boardManager.serchPositions[Random.Range(0, boardManager.serchPositions.Count)];
            AIPlaceTile(Check8Directions(pos));

        }
    }

    private Vector3Int Check8Directions(Vector3Int pos)
    {
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                Vector3Int newPos = pos + new Vector3Int(i, j, 0);
                if(boardManager.CanPlaceTile(newPos))
                {
                    return newPos;
                }
            }
        }

        return new Vector3Int(0,0,0);
    }

    /// <summary>
    /// If no positions containing valid neighbours were found, AI instantiates at random empty position.
    /// </summary>
    private void FindRandomPosition()
    {
        AIPlaceTile(emptyTiles[Random.Range(0, emptyTiles.Count)]);
    }

    private void AIPlaceTile(Vector3Int pos, E_Symbol symb)
    {
        GameObject tile = AIPlaceTile(pos);
        tile.transform.GetComponentInChildren<AISymbol>().SetSymbol(symb);
    }

    private GameObject AIPlaceTile(Vector3Int pos)
    {
        boardManager.TryPlaceTile(pos);
        return Instantiate(resources.GetAITile(), boardManager.gameboard.GetCellCenterWorld(pos), Quaternion.identity);
    }
}
