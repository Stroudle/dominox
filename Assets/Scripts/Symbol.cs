using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public E_Symbol symbol { get; private set; }
    private SpriteRenderer spRenderer;

    private void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SO_Symbol symbolData = ResourceManager.Instance.GetSymbolData((E_Symbol)Random.Range(0, 2));
        symbol = symbolData.symbol;
        spRenderer.sprite = symbolData.sprite;
        spRenderer.color = symbolData.color;
    }
}
