using UnityEngine;

public class PlayerTileSpawn : MonoBehaviour
{
    private ResourceManager resources;
    private Vector3 spawnPoint;

    private void Awake()
    {
        resources = ResourceManager.Instance;
    }

    private void Start()
    {
        PlayerDrag.OnDragStart += DragStartEventHandler;
        PlayerDrag.OnPlayerPlaceTile += PlaceTileEventHandler;
    }

    private void DragStartEventHandler(Vector3 pos)
    {
        spawnPoint = pos;
    }

    private void PlaceTileEventHandler()
    {
        SpawnTile();
    }

    private void SpawnTile()
    {
        Instantiate(resources.GetPlayerTile(), spawnPoint, Quaternion.identity);
    }
}
