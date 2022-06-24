using System;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool isDrag;
    private Vector3 startPos,dragStartMousePos, dragStartPos;
    private GameboardManager boardManager;

    private void Awake()
    {
        startPos = transform.position;
        boardManager = ResourceManager.Instance.boardManager;
        Debug.Log(boardManager);
    }

    private void OnMouseDown()
    {
        isDrag = true;
        dragStartMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragStartPos = transform.position;
    }

    //Update is faster than OnMouseDrag.
    //private void OnMouseDrag()
    //{
    //}

    private void Update()
    {
        if(isDrag)
        {
            transform.position = dragStartPos + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragStartMousePos);
        }
    }

    private void OnMouseUp()
    {
        isDrag = false;

        if(boardManager.CanPlaceTile(transform.position))
        {
            SnapToTile();
        }
        else
        {
            transform.position = startPos;
        }
    }

    private void SnapToTile()
    {
        transform.position = boardManager.gameboard.GetCellCenterWorld(boardManager.gameboard.WorldToCell(transform.position));

        Destroy(this);
        Destroy(GetComponent<BoxCollider2D>());
    }
}
