using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;

/*
 * https://github.com/ChrisMaire/unity-native-sharing
 */

public class Sharing : MonoBehaviour
{
    private string ScreenshotName = "screenshot.png";
    private string url = "";
    public static Sharing sharing;

    public delegate void IsShared();
    public static IsShared AppShared;

    void Start()
    {
        sharing = this;
    }

    #region Sharing Screen Shot

    public void ShareScreenshotWithText(Texture2D texture, bool isFB = false)
    {
        url = "https://play.google.com/store/apps/details?id=" + Application.identifier;
        string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        if (File.Exists(screenShotPath))
            File.Delete(screenShotPath);

        if (texture == null)
        {           
            ScreenCapture.CaptureScreenshot(ScreenshotName);
        }
        else
        {
            var bytes = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(screenShotPath, texture.EncodeToPNG());
            File.WriteAllBytes(screenShotPath, bytes);
        }

        if (isFB)
            StartCoroutine(DelayedFacebookShare(screenShotPath));
        else
            StartCoroutine(delayedShare(screenShotPath));
    }

    //CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
    //for it to save, or set a unique image name and check if the file has been created yet before sharing.

    IEnumerator delayedShare(string screenShotPath)
    {
        while (!File.Exists(screenShotPath))
            yield return new WaitForSeconds(.05f);

        new NativeShare().SetCallback(ShareCallBack).AddFile(screenShotPath).SetSubject(Application.productName).SetText("Explore and release your Inner artist as did I" + "\n" + url + "\n").Share();
    }

    /// <summary>
    /// Direct Facebook Sharing
    /// </summary>
    /// <param name="screenShotPath"></param>
    /// <returns></returns>
    /// 
    IEnumerator DelayedFacebookShare(string screenShotPath)
    {
        while (!File.Exists(screenShotPath))
            yield return new WaitForSeconds(.05f);

        //Debug.LogError (NativeShare.)

        if (NativeShare.TargetExists("com.facebook.orca") || NativeShare.TargetExists("com.facebook.katana") ||
            NativeShare.TargetExists("com.example.facebook") || NativeShare.TargetExists("com.facebook.android")  )
            new NativeShare().SetCallback(ShareCallBack).AddTarget("com.facebook.katana").AddFile(screenShotPath).Share();
        else
            print("Facebook Isn't installed at this device !");
    }

    /// <summary>
    ///  If User Share the Thing and you wanna give reward
    /// </summary>
    /// <param name="shareResult"></param>
    /// <param name="shareTarget"></param>
    /// 
    public void ShareCallBack(NativeShare.ShareResult shareResult, string shareTarget)
    {
        
        if (shareResult == NativeShare.ShareResult.Shared)
        {
            if (AppShared != null)
            {
                AppShared();
                AppShared = null;
            }
        }
    }

    #endregion

    #region Rarely Used Things
    //---------- Helper Variables ----------//
    private float width
    {
        get
        {
            return Screen.width;
        }
    }

    private float height
    {
        get
        {
            return Screen.height;
        }
    }

    //---------- Screenshot ----------//
    public void Screenshot()
    {
        // Short way
        StartCoroutine(GetScreenshot());
    }

    //---------- Get Screenshot ----------//
    public IEnumerator GetScreenshot()
    {
        yield return new WaitForEndOfFrame();

        // Get Screenshot
        Texture2D screenshot;
        screenshot = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
        screenshot.Apply();

        // Save Screenshot
        Save_Screenshot(screenshot);
    }

    //---------- Save Screenshot ----------//
    private void Save_Screenshot(Texture2D screenshot)
    {
        string screenShotPath = Application.persistentDataPath + "/" + DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss") + "_" + ScreenshotName;
        File.WriteAllBytes(screenShotPath, screenshot.EncodeToPNG());

        // Native Share
        StartCoroutine(DelayedShare_Image(screenShotPath));
    }

    //---------- Clear Saved Screenshots ----------//
    public void Clear_SavedScreenShots()
    {
        string path = Application.persistentDataPath;

        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.png");

        foreach (FileInfo f in info)
        {
            File.Delete(f.FullName);
        }
    }

    //---------- Delayed Share ----------//
    private IEnumerator DelayedShare_Image(string screenShotPath)
    {
        while (!File.Exists(screenShotPath))
        {
            yield return new WaitForSeconds(.05f);
        }

        // Share
        NativeShare_Image(screenShotPath);
    }

    //---------- Native Share ----------//
    private void NativeShare_Image(string screenShotPath)
    {
//        string text = "";
//        string subject = "";
//        string url = "";
//        string title = "Select sharing app";

//#if UNITY_ANDROID

//        subject = "Test subject.";
//        text = "Test text";
//#endif

//#if UNITY_IOS
//		subject = "Test subject.";
//		text = "Test text";
//#endif


        // Share
        new NativeShare().AddFile(screenShotPath).SetSubject("Subject goes here").SetText("Hello world!").Share();
    }
    #endregion

}

