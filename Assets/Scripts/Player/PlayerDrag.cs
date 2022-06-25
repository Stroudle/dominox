using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDrag : MonoBehaviour
{
    public bool isDrag { get; private set; }
    private Vector3 startPos,dragStartMousePos, dragStartPos;
    private GameboardManager boardManager;

    public static UnityAction OnPlaceTile;
    public static UnityAction<Vector3> OnDragStart;

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
        Destroy(GetComponent<BoxCollider2D>());

        OnDragStart(dragStartPos);
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

        if(boardManager.TryPlaceTile(transform.position))
        {
            PlaceTile();
        }
        else
        {
            transform.position = startPos;
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    private void PlaceTile()
    {
        transform.position = boardManager.SnapToGrid(transform.position);

        OnPlaceTile();

        Destroy(GetComponent<PlayerInput>());
        Destroy(this);
    }
}
