using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRow : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Stage[] stageObjs;
    [SerializeField] private GameObject linkRow;

    public void Init(int row, bool showLinkRow)
    {
        _rectTransform.sizeDelta = new Vector2(568, 100);
        linkRow.SetActive(showLinkRow);
        for (int i = 0; i < stageObjs.Length; i++)
        {
            int stage = row * stageObjs.Length + i;
            stageObjs[i].Init(stage, stage <= Data.LevelPassed, Data.GetStageStars(stage));
        }
    }

    public int index;
    void ScrollCellIndex (int idx)
    {
        index = StageView.I.maxStage - idx - 1;
        Init(index, index != StageView.I.maxStage && index != 0);
    }
}
