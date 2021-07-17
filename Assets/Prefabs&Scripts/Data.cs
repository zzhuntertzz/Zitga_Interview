using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static int LevelPassed
    {
        get
        {
            return PlayerPrefs.GetInt("LevelPassed", 0);
        }
        set
        {
            PlayerPrefs.SetInt("LevelPassed", value);
        }
    }
    
    public static int StarTotalGot
    {
        get
        {
            int total = 0;
            int pass = LevelPassed;
            for (int i = 0; i < pass; i++)
            {
                total += GetStageStars(i);
            }
            return total;
        }
    }

    public static int CurrentLevel = 0;
    
    public static int GetStageStars(int index)
    {
        return PlayerPrefs.GetInt($"Stage_{index}", 0);
    }

    public static void SetStageStars(int index, int star)
    {
        PlayerPrefs.SetInt($"Stage_{index}", star);
    }
}
