using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MazeGame : MonoBehaviour
{
    public static MazeGame I;
    private void Awake()
    {
        I = this;
    }

    [SerializeField] private Button hintBtn;
    [SerializeField] private MazeRenderer mazeRenderer;
    [SerializeField] private Slider timeSl;
    [SerializeField] private int maxTime = 60;
    [SerializeField] [Range(1, 60)] private int[] scorePoint;
    
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    [SerializeField] private Text starGotTxt;

    private void Start()
    {
        timeSl.maxValue = maxTime;
        timeSl.value = maxTime;
        
        hintBtn.onClick.AddListener(delegate
        {
            hintBtn.interactable = false;
            mazeRenderer.ShowHint();
        });

        timeSl.DOValue(0, maxTime).OnComplete(delegate
        {
            Lose();
        });
    }

    public void Win()
    {
        win.SetActive(true);
        int score = CalcScore();
        starGotTxt.text = $"Got {score.ToString()} stars";
        Data.SetStageStars(Data.CurrentLevel, score);
        DOTween.KillAll();
        if (Data.LevelPassed <= Data.CurrentLevel)
        {
            Data.LevelPassed++;
        }
    }

    int CalcScore()
    {
        for (int i = scorePoint.Length - 1; i >= 0; i--)
        {
            if (timeSl.value > scorePoint[i]) return i + 1;
        }

        return 0;
    }

    void Lose()
    {
        lose.SetActive(true);
        DOTween.KillAll();
    }

    public void Quit()
    {
        SceneManager.LoadScene("Stage");
    }
}
