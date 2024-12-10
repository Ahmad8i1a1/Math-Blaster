using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PauseBtn, ResumeBtn, HomeBtn, HintBtn;
    public GameObject PausePnl, WinPnl, CurPnl, ExitPnl, HighScoreCat, WinShine;

    [SerializeField] Text Level , Score ;

    private bool isPnlOpen = false;
    //Timer
    [SerializeField] Text countDownText;
    public static float remainingTime;
    [SerializeField] Slider countDownSlider;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = 30;
        countDownSlider.maxValue = 0;
        countDownSlider.minValue = -remainingTime;

        Debug.Log(GlobalValues.Operation+"From GameManager");
        Debug.Log(GlobalValues.Level+"From Gamemanager");

    }

    // Update is called once per frame
    void Update()
    {
        upgradeLevel();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurPnl.activeSelf == true)
                closepanel(CurPnl);
            else
                if (ExitPnl.activeInHierarchy == false)
                openPanel(ExitPnl);
        }

        Level.text = "Level : " + GlobalValues.Level;

        if (!isPnlOpen)
        {
            Timer();
        }


        if(remainingTime == 0)
        {
            WinShow();
        }

    }

    //WinPnl
    #region WinPnl
    public void WinShow()
    {
        WinPnl.SetActive(true);
        WinShine.transform.Rotate(0, 0, 30); 
        iTween.ScaleTo(WinShine, iTween.Hash("scale", Vector3.one, "time", 1f, "easetype", iTween.EaseType.easeOutElastic));
        //SoundsManager.controller.completionSound();
        Score.text= (GlobalValues.Level).ToString();
        if (GlobalValues.Level > GlobalValues.isHighScore)
        {
            HighScoreCat.SetActive(false);
            GlobalValues.isHighScore = GlobalValues.Level;
        }

    }
    public void TryAgain()
    {
        SoundsManager.controller.click();
        SceneManager.LoadScene("GamePlay");
        GlobalValues.Level = 0;
    }
    public void Wintohome()
    {
        SoundsManager.controller.click();
        SceneManager.LoadScene("Home");
        GlobalValues.Level = 0;
    }
    #endregion

    #region ExitPnl
    public void Exityes()
    {
        SoundsManager.controller.click();
        GlobalValues.Level = 0;
        Application.Quit();
    }
    public void Exitno()
    {
        SoundsManager.controller.click();
        closepanel(CurPnl);
    }
    #endregion

    //Pause
    #region Pause
    public void pause()
    {
        SoundsManager.controller.panelSound();
        openPanel(PausePnl);
    }

    public void resume() { 
        SoundsManager.controller.click();
        closepanel(PausePnl);
    }
    public void pausetohome()
    {
        SoundsManager.controller.click();
        SceneManager.LoadScene("Home");
        GlobalValues.Level = 0;
    }
    #endregion

    //Upgrade
    #region Upgrade
    public void upgradeLevel()
    {

        if (GlobalValues.Level % 5 == 0 && MathManager.upperLimit <= 1001 && MathManager.answerState==true)
        {
            MathManager.upperLimit += 50;
            MathManager.lowerLimit += 50;
        }

        if (GlobalValues.Level % 10 == 0 && remainingTime <= 15 && MathManager.answerState == true)
        {
            remainingTime -= 5;
        }
    }
    #endregion

    //Timer
    #region Timer
    private void Timer()
    {

        countDownSlider.value = remainingTime;
        // https://www.youtube.com/watch?v=POq1i8FyRyQ
        remainingTime -= Time.deltaTime;
        if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        int Mints = Mathf.FloorToInt(remainingTime / 60);
        int Sec = Mathf.FloorToInt(remainingTime % 60);
        countDownText.text = string.Format("Time {0:00}:{1:00}", Mints, Sec);
        countDownSlider.value = -remainingTime;
    }
    #endregion

    //Panel Animation
    #region Panel Animation
    public void openPanel(GameObject panel)
    {
        isPnlOpen = true;
        SoundsManager.controller.panelSound();
        CurPnl = panel;
        CurPnl.SetActive(true);
        CurPnl.transform.parent.gameObject.SetActive(true);
        CurPnl.transform.localScale = Vector3.one * .3f;
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", .75f, "time", .7f, "onUpdate", nameof(FadeAnimation), "easeType", iTween.EaseType.linear));
        iTween.ScaleTo(CurPnl, iTween.Hash("scale", Vector3.one, "time", 1f, "easetype", iTween.EaseType.easeOutElastic));
    }
    public void closepanel(GameObject panel)
    {
        SoundsManager.controller.panelSound();
        iTween.ValueTo(gameObject, iTween.Hash("from", .75f, "to", 0, "time", .5f, "onUpdate", nameof(FadeAnimation), "easeType", iTween.EaseType.linear, "oncomplete", nameof(deactivePanel), "oncompletetarget", gameObject, "oncompleteparams", panel));
        iTween.ScaleTo(CurPnl, iTween.Hash("scale", Vector3.one * .75f, "time", .7f, "easetype", iTween.EaseType.easeOutElastic));
    }
    public void FadeAnimation(float amount) => CurPnl.transform.parent.GetComponent<Image>().color = new Color(0f, 0f, 0f, amount);
    public void deactivePanel(GameObject panel)
    {
        isPnlOpen = false;
        panel.transform.parent.gameObject.SetActive(false);
        panel.SetActive(false);
    }
    #endregion
}
