using UnityEngine;

[CreateAssetMenu(fileName = "symbol_", menuName = "Scriptable/SO_symbol")]
public class SO_Symbol : ScriptableObject
{
    public E_Symbol symbol;
    public Sprite sprite;
    public Color color;
}