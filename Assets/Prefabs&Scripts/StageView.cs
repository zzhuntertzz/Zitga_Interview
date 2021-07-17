using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;
using UnityEngine.UI;

public class StageView : MonoBehaviour
{
    private static StageView i;
    public static StageView I
    {
        get
        {
            if (i == null) i = FindObjectOfType<StageView>();
            return i;
        }
    }

    [SerializeField] private Text starGotTxt;
    [SerializeField] private InitOnStart InitOnStart;
    internal int maxStage = 999;
    
    // Start is called before the first frame update
    void Awake()
    {
        maxStage = maxStage / 4 + 1;
        InitOnStart.totalCount = maxStage;
        starGotTxt.text = Data.StarTotalGot.ToString();
    }
}
