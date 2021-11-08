using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReReel : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] private RectTransform[] GameObject;

    [SerializeField] private readonly float exitPos = 202;
    [SerializeField] private float sizeSymbol = 200;
    [SerializeField] private float quantitySymbol = 3;
    [SerializeField] private float const1 = 580;

    [SerializeField] private Sprite[] sprites;
    private int currentSymbolIndex=0;
    private int currentFinalSet = 0;

    [SerializeField] public int ReelID;
private ReelState reelState = global::ReelState.Stop;

    internal ReelState ReelState { get => reelState; set => reelState = value; }

    private void Update()
    {
        foreach (var item in GameObject)
        {
            
            if (item.position.y <= exitPos)
            {
                MovieSumbolUp(item);
                ChangedSumbolSprite(item);
            }
        }
    }
    public void ResetSumbolPos(float reelstartposY)
    {
        currentSymbolIndex = 0;
        if (currentFinalSet<gameConfig.FinalScrins.Length-1)
        {
            currentFinalSet++;
        }
        else
        {
            currentFinalSet = 0;
        }
        foreach (var item in GameObject)
        {
            var sPosY = item.localPosition;
            var newpos = sPosY.y+ reelstartposY;
            item.localPosition = new Vector3(sPosY.x, newpos);

        }
    }
    public void MovieSumbolUp(RectTransform sumbolRT)
    {
        // var offset = item.position.y + 800/*200 * 4*/;
        var offset = /*const1*/ sumbolRT.position.y + (sizeSymbol * quantitySymbol);
        print(offset);
        var newpos = new Vector3(sumbolRT.position.x, offset);
        sumbolRT.position = newpos;
    }
    private void ChangedSumbolSprite(RectTransform sumbol)
    {
        if (ReelState == ReelState.Stopping || reelState == ReelState.ForceStopping)
        {
            sumbol.GetComponent<Image>().sprite = GetFinalScreenSymbol();
            currentSymbolIndex++;
        }
        else
        {
            sumbol.GetComponent<Image>().sprite = GetRandomSymbols();

            
        }
        //var random = Random.Range(0,sprites.Length);
        //sumbol.GetComponent<Image>().sprite = sprites[random];
    }
    private Sprite GetRandomSymbols()
    {
        var random = Random.Range(0,gameConfig.Symbols.Length);
        var sprite = gameConfig.Symbols[random].SymbolImage;
        return sprite;
    }
    private Sprite GetFinalScreenSymbol()
    {
        var finalScreenSymbolIndex = currentSymbolIndex + (ReelID - 1) * 3/*gameConfig.VisibleSymbolsOnReel*/;
        var currentFinalScreen = gameConfig.FinalScrins[currentFinalSet].FinalScreen;
        if (finalScreenSymbolIndex >=currentFinalScreen.Length)
        {
            finalScreenSymbolIndex = 0;
        }
        var newSumbol = gameConfig.Symbols[currentFinalScreen[finalScreenSymbolIndex]];
        return newSumbol.SymbolImage;
    }
}
