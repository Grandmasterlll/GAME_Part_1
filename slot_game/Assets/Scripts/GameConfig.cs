using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Config", menuName = "Game Config")]
public class GameConfig : ScriptableObject
{

    [SerializeField] private SymbolData[] symbols;
    [SerializeField] private FinalScrinsData[] finalScrins;

    [SerializeField] private int visibleSymbolsOnReel;

    public SymbolData[] Symbols  => symbols; 
    public FinalScrinsData[] FinalScrins  => finalScrins;

    public int VisibleSymbolsOnReel => visibleSymbolsOnReel; 
}
