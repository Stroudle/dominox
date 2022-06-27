using UnityEngine;

public class BaseSymbol : MonoBehaviour
{
    public E_Symbol symbol { get; protected set; }
    private SpriteRenderer spRenderer;
    private const int NUMELEMENTS = 2;

    private void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        symbol = (E_Symbol)Random.Range(0, NUMELEMENTS);
        Config();
    }

    protected void Config()
    {
        SO_Symbol symbolData = ResourceManager.Instance.GetSymbolData(symbol);
        symbol = symbolData.symbol;
        spRenderer.sprite = symbolData.sprite;
        spRenderer.color = symbolData.color;
    }
}
