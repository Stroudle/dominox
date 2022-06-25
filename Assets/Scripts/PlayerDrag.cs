using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDrag : MonoBehaviour
{
    private bool isDrag;
    private Vector3 startPos,dragStartMousePos, dragStartPos;
    private GameboardManager boardManager;

    public static UnityAction<Vector3> OnPlaceTile;

    private void Awake()
    {
        startPos = transform.position;
        boardManager = ResourceManager.Instance.boardManager;
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
            PlaceTile();
        }
        else
        {
            transform.position = startPos;
        }
    }

    private void PlaceTile()
    {
        transform.position = boardManager.SnapToGrid(transform.position);
        Destroy(GetComponent<BoxCollider2D>());

        OnPlaceTile(transform.position);

        Destroy(this);
    }
}
