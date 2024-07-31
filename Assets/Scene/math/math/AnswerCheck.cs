using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnswerCheck : MonoBehaviour
{
    public GameObject btn1, btn2, btn3;
    [SerializeField] private float timeForHintAnimation = 1.5f;
    [SerializeField] private float scaleMultiplierForHint = 1.1f;

    private void hint()
    {
        if (btn1.GetComponentInChildren<Text>().text == MathManager.ans.ToString())
        {
        animationForAns(btn1);
        }
        else if(btn2.GetComponentInChildren<Text>().text == MathManager.ans.ToString())
        {
        animationForAns(btn2);
        }
        else if(btn3.GetComponentInChildren<Text>().text == MathManager.ans.ToString())
        {
        animationForAns(btn3);
        }
    }
    public void AnswerChecker1()
    {
        if (btn1.GetComponentInChildren<Text>().text == MathManager.ans.ToString())
        {
            Debug.Log("Correct Answer");
            MathManager.answerState = true;
        }
        else
        {
            Debug.Log("Wrong Answer");
        }
    }
    public void AnswerChecker2()
    {

        if (btn2.GetComponentInChildren<Text>().text == MathManager.ans.ToString())
        {
            Debug.Log("Correct Answer");
            MathManager.answerState = true;
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

    }
    public void AnswerChecker3()
    {
        if (btn3.GetComponentInChildren<Text>().text == MathManager.ans.ToString())
        {
            Debug.Log("Correct Answer");
            MathManager.answerState = true;
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

    }
    public void upgradeLevel()
    {
        //if (GlobalValues.Level % 5 == 0 && MathManager.upperLimit <= 1001)
        //{
        //    MathManager.upperLimit += 51;
        //}
    }
    void animationForAns(GameObject button)
    {
        iTween.ScaleTo(button, iTween.Hash(
            "scale", Vector3.one * scaleMultiplierForHint,
            "time", timeForHintAnimation,
            "easetype", iTween.EaseType.linear,
            "LoopType", iTween.LoopType.pingPong, //for repeating process e.g. animation
            "islocal", true
            ));
    }
}
