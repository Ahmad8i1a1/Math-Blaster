using UnityEngine;
using System.Collections.Generic;

public class GlobalValues
{
    public static bool isPrefabsActice = false;
    public static char Operation = '+';
    public static int Level = 0;
    public static int HighScore = 0;

    public static int isHighScore
    {
        get => PlayerPrefs.GetInt("HighScore", 0);
        set => PlayerPrefs.SetInt("HighScore", value);
    }
    public static int isMusic
    {
        get => PlayerPrefs.GetInt("IsMusicOn", 1);
        set => PlayerPrefs.SetInt("IsMusicOn", value);
    }
    public static int isSound
    {
        get => PlayerPrefs.GetInt("IsSoundOn", 1);
        set => PlayerPrefs.SetInt("IsSoundOn", value);
    }
 
}