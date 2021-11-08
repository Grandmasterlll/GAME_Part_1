using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ReelScroll : MonoBehaviour
{
    [SerializeField] private ReReel[] rereel;
    private Dictionary<RectTransform, ReReel> reelDictionary;

    [SerializeField] private RectTransform[] reels;
    [SerializeField] private float delaySteep;
    [SerializeField] private Ease startEase;
    [SerializeField] private Ease stopEase;
    [SerializeField] private float startDist, linearDist,stopDist;
    [SerializeField] private float startD, linearD, stopD;
    [SerializeField] private Button playButton;
    [SerializeField] private RectTransform playButtonrt;
    [SerializeField] private Button stopButton;
    [SerializeField] private RectTransform stopButtonrt;
    private float reelstartposY;
    private float thirdReelPosx;
    [SerializeField] private float symbolHeight;
    [SerializeField] private int visibleSymbolsOnReel;
    //private float reelLenth= reels.Length
    private void Start()
    {
        stopButton.interactable = false;
        stopButtonrt.localScale = Vector3.zero;

        print(reels.Length);
        reelDictionary = new Dictionary<RectTransform, ReReel>();
        for (int i = 0; i < reels.Length; i++)
        {
            reelDictionary.Add(reels[i], rereel[i]);
        }
        print(reels.Length);
        thirdReelPosx = reels[1].position.x;
        reelstartposY = reels[0].localPosition.y;
    }

    public void ScrollStartReel()
    {
        playButton.interactable = false;
        playButtonrt.localScale = Vector3.zero;
        stopButtonrt.localScale = Vector3.one;
        for (int i = 0; i < reels.Length; i++)
        {
            var reel = reels[i];
            reel.DOAnchorPosY(startDist, startD).SetDelay(i*delaySteep).SetEase(startEase).OnComplete(()=> 
                {
                    Scrolllinear(reel);
                    if (reelDictionary[reel].ReelID==reels.Length)
                    {
                        stopButton.interactable = true;
                    }
                });

        }
    }
    private void Scrolllinear(RectTransform reels)
    {
        reelDictionary[reels].ReelState = ReelState.Spin;
        DOTween.Kill(reels);
        reels.DOAnchorPosY(linearDist, linearD).SetEase(Ease.Linear).OnComplete(() => CorrectReelPos(reels));
    }
    private void Scrollstop(RectTransform reels)
    {
        reelDictionary[reels].ReelState = ReelState.Stopping;
        DOTween.Kill(reels);
        var reelCurentPosY = reels.localPosition.y;
       // var exctraDistance = CalculatesExtraDistance(reelCurentPosY);

        var sloDownDistance = reelCurentPosY - symbolHeight * visibleSymbolsOnReel /*- exctraDistance*/;
        reels.DOAnchorPosY(sloDownDistance /*stopDist*/, stopD)
            .SetEase(stopEase)
            .OnComplete(() => 
            {
                reelDictionary[reels].ReelState = ReelState.Stop;
                NextReel(reels);
                if (reels.position.x == thirdReelPosx  /*reelDictionary[reels].ReelID == reels*/)
                {
                    // playButton.interactable = true;
                    stopButton.interactable = false;
                    stopButtonrt.localScale = Vector3.zero;
                    
                    playButton.interactable = true;
                    playButtonrt.localScale = Vector3.one;


                }
            });
    }

    private void NextReel(RectTransform reels)
    {
        var curentreelpos = reels.localPosition.y;
        reels.localPosition = new Vector3(reels.localPosition.x, reelstartposY);
        reelDictionary[reels].ResetSumbolPos(curentreelpos);

    }
    public void ForceScrollStop()
    {
        stopButton.interactable = false;
        foreach (var item in reels)
        {
            if (reelDictionary[item].ReelState==ReelState.Spin)
            {
                CorrectReelPos(item);
            }
            //stop(item);
        }
    }

    private float CalculatesExtraDistance(float reelCurentPosY)
    {
        var traveledDistance = reelstartposY - reelCurentPosY;
        var symbolsScrilled = traveledDistance / symbolHeight;
        var integerPart = Mathf.Floor(symbolsScrilled);
        var fractionalPart = symbolsScrilled - integerPart;
        var extraDistance = (1 - fractionalPart) * symbolHeight;
        return extraDistance;
    }
    private void CorrectReelPos(RectTransform reelrt)
    {
        DOTween.Kill(reelrt);
        var currentReelPos = reelrt.localPosition.y;
        var extraDistance = CalculatesExtraDistance(currentReelPos);
        var correctionDistance = currentReelPos - extraDistance;
        var correctionDuration = extraDistance / -(linearDist/linearD);
        reelrt.DOAnchorPosY(correctionDistance, correctionDuration).OnComplete(() => Scrollstop(reelrt));
    }
  
}
