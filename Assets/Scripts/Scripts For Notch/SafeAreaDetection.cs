using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaDetection : MonoBehaviour
{
    public delegate void SafeAreaChanged(Rect safeArea);
    public static event SafeAreaChanged OnSafeAreaChanged;

    private Rect _safeArea;
 
    private void Awake()
    {
        _safeArea = Screen.safeArea;
    }

    // Update is called once per frame
    private void Start()
    {
        if (_safeArea != Screen.safeArea)
        {
            _safeArea = Screen.safeArea;
            OnSafeAreaChanged?.Invoke(_safeArea);
        }
    }
}
