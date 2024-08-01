using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Home : MonoBehaviour
{
    public GameObject PlayBtn, SettingBtn, ShopBtn;
    public GameObject SettingPnl, ExitPnl, LevelPnl, ShopPnl, HighScoreBoard;
    public Text HighScore ;

    private bool isPnlOpen = false;
    [Header("UI Pnl's")]
    public GameObject CurPnl;

    [Header("Prefabs")]
    public GameObject Sound;
     
    [Header("Setting")]
    public Image[] SettingIcons;
    public Sprite[] OnOffIcon;

    void Awake()
    {
        if (!GlobalValues.isPrefabsActice)
        {
            DontDestroyOnLoad(Instantiate(Sound));
            GlobalValues.isPrefabsActice = true;
        }
        iTween.ScaleTo(PlayBtn, iTween.Hash("scale", Vector3.one, "time", 0.5f, "easetype", "spring", "delay", 0.2f, "oncomplete", nameof(LowerBar), "oncompletetarget", gameObject));
        InitialSetting();
    }

    //ScenAnim
    #region ScenAnim
    void LowerBar()
    {
        iTween.ScaleTo(PlayBtn, iTween.Hash("scale", Vector3.one * 1.1f, "time", 1f, "easetype", "spring", "looptype", "pingpong"));
    }
    #endregion

    //ExitPnl
    #region ExitPnl
    public void Exityes()
    {
        SoundsManager.controller.click();
        Application.Quit();
    }
    public void Exitno()
    {
        SoundsManager.controller.click();
        closepanel(CurPnl);
    }
    #endregion

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurPnl.activeSelf == true)
                closepanel(CurPnl);
            else
                if (ExitPnl.activeInHierarchy == false)
                openPanel(ExitPnl);
        }
    }

    private void Start()
    {
        HighScore.text = (GlobalValues.HighScore*100).ToString();
    }

    //Setting
    #region Setting Panel
    public void OpenSettingPnl()
    {
        if (isPnlOpen) return;
        SoundsManager.controller.click();
        openPanel(SettingPnl);
    }
    void InitialSetting()
    {
        SettingIcons[0].sprite = OnOffIcon[GlobalValues.isMusic];
        SettingIcons[1].sprite = OnOffIcon[GlobalValues.isSound];
    }
    public void musicController()
    {
        GlobalValues.isMusic = 1 - GlobalValues.isMusic;
        SettingIcons[0].sprite = OnOffIcon[GlobalValues.isMusic];
        SoundsManager.controller.music(!Convert.ToBoolean(GlobalValues.isMusic));
    }
    public void soundController()
    {
        GlobalValues.isSound = 1 - GlobalValues.isSound;
        SettingIcons[1].sprite = OnOffIcon[GlobalValues.isSound];
        SoundsManager.controller.sound(!Convert.ToBoolean(GlobalValues.isSound));
    }
    #endregion

    //Share
    #region Share    
    public void shareUs()
    {
        SoundsManager.controller.click();
        Application.OpenURL("https://play.google.com/store/apps");
    }
    #endregion

    //levelSelection
    #region LevelSelection
    public void Play()
    {
        if (isPnlOpen) return;
        SoundsManager.controller.click();
        LevelPnl.SetActive(true);
        
    }
    public void closeLevelPnl()
    {
        if (isPnlOpen) return;
        SoundsManager.controller.click();
        LevelPnl.SetActive(false);
    }

    //Button

    public void plus()
    {
        SoundsManager.controller.click();
        GlobalValues.Operation = '+';
        SceneManager.LoadScene("GamePlay");
        Debug.Log("+ from main menu ");
    }
    public void minus()
    {
        SoundsManager.controller.click();
        GlobalValues.Operation = '-';
        SceneManager.LoadScene("GamePlay");
        Debug.Log("- from main menu ");
    }
    public void multiply()
    {
        SoundsManager.controller.click();
        GlobalValues.Operation = '*';
        SceneManager.LoadScene("GamePlay");
        Debug.Log("* from main menu ");
    }
    public void divide()
    {
        SoundsManager.controller.click();
        GlobalValues.Operation = '/';
        SceneManager.LoadScene("GamePlay");
        Debug.Log("/ from main menu ");
    }
    #endregion

    //Store
    #region Store
    public void OpenShopPanal()
    {
        if (isPnlOpen) return;
        SoundsManager.controller.click();
        ShopPnl.SetActive(true);
        Debug.Log("working on it but open");
    }
    public void CloseShopPanel()
    {
        if (isPnlOpen) return;
        SoundsManager.controller.click();
        ShopPnl.SetActive(false);
        Debug.Log("working on it but close");
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