using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReReel : MonoBehaviour
{
    [SerializeField] private RectTransform[] GameObject;

    [SerializeField] private readonly float exitPos = 202;
    [SerializeField] private float sizeSymbol = 200;
    [SerializeField] private float quantitySymbol = 3;
    [SerializeField] private float const1 = 580;

    [SerializeField] private Sprite[] sprites;

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
        var random = Random.Range(0,sprites.Length);
        sumbol.GetComponent<Image>().sprite = sprites[random];
    }
}
