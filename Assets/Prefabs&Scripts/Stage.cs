using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    [SerializeField] private Text stageTxt;
    [SerializeField] private GameObject[] lstStars;
    [SerializeField] private Button playBtn;
    [SerializeField] private GameObject lockObj;
    private int stage = 0;

    private void Start()
    {
        playBtn.onClick.AddListener(PlayStage);
    }

    private void PlayStage()
    {
        Data.CurrentLevel = stage;
        SceneManager.LoadScene("Game");
    }

    public void Init(int stage, bool canPlay, int starGot)
    {
        this.stage = stage;

        tutorial.SetActive(stage == 0);
        for (int i = 0; i < lstStars.Length; i++)
        {
            lstStars[i].SetActive(i < starGot);
        }
        stageTxt.text = stage.ToString();
        lockObj.SetActive(!canPlay);
        playBtn.interactable = canPlay;
    }
}
