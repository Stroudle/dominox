using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISymbol : BaseSymbol
{
    public void SetSymbol(E_Symbol symb)
    {
        symbol = symb;
        Config();
    }
}
