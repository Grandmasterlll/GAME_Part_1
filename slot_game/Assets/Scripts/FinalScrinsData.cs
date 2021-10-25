using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Final Scrins", menuName = "Final Scrins")]
public class FinalScrinsData : ScriptableObject
{
    [SerializeField] private int[] finalScreen;

    public int[] FinalScreen  => finalScreen; 
}
