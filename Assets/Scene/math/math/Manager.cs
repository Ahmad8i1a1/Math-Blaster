using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    [SerializeField]  Text countDownText;
    [SerializeField]  float remainingTime = 90 ;
    [SerializeField] Slider countDownSlider;
    private bool stop = false;

    private void Start()
    {
        countDownSlider.maxValue= 0;
        countDownSlider.minValue = - remainingTime;

        //if (GlobalValues.Level % 10 == 0 && remainingTime != 10)
        //{
        //    remainingTime -= 10;
        //}
    }
    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        
        countDownSlider.value = remainingTime;
        //https://www.youtube.com/watch?v=POq1i8FyRyQ
        remainingTime -= Time.deltaTime;
        if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        int Mints = Mathf.FloorToInt(remainingTime / 60);
        int Sec = Mathf.FloorToInt(remainingTime % 60);
        countDownText.text = string.Format("Time {0:00}:{1:00}",Mints,Sec);
        countDownSlider.value = - remainingTime;
    }
}
