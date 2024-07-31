using UnityEngine;
using System.Collections.Generic;

public class GlobalValues
{
    public static bool isPrefabsActice = false;
    //public static List<string> appUrls;
    public static char Operation = '+';
    public static int Level = 0;
    public static int HighScore = 0;
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