using UnityEngine;
using UnityEngine.UI;
public class MathManager : MonoBehaviour
{

    private int num1, num2;
    public static float ans;
    public static int upperLimit, lowerLimit;
    [SerializeField] Text question;
    [SerializeField] Text option1;
    [SerializeField] Text option2;
    [SerializeField] Text option3;
    //char operation = '+';
    int count;
    int wrong1, wrong2;
    public static bool answerState= false;
    // Start is called before the first frame update
    void Start()
    {
        lowerLimit = 1;
        upperLimit = 51;
        NumGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        if (answerState)
        {
            NumGeneration();
            answerState = false;
        }

        QuestionGeneration();
        OptionGeneration();
    }

    public void NumGeneration()
    {
        GlobalValues.Level ++ ;
        num1 = Random.Range(lowerLimit, upperLimit);
        num2 = Random.Range(lowerLimit, upperLimit);
        count = Random.Range(1, 4);
        wrong1 = Random.Range(lowerLimit, upperLimit);
        wrong2 = Random.Range(lowerLimit, upperLimit);
        if (wrong1 == wrong2 || wrong1 == num1 || wrong1 == num2 ||
            wrong2 == num1 || wrong2 == num2 ||
            num1 == num2)
        {
            NumGeneration();
        }

    }

    void QuestionGeneration()
    {
        question.text = "Q: " + num1 + " " + GlobalValues.Operation + " " + num2;

    }

    void OptionGeneration()
    {
        if (count == 1)
        {
            option1.text = CorrectAns().ToString();
            option2.text = wrong1.ToString();
            option3.text = wrong2.ToString();
        }
        else if (count == 2)
        {
            option2.text = CorrectAns().ToString();
            option1.text = wrong1.ToString();
            option3.text = wrong2.ToString();
        }
        else
        {
            option3.text = CorrectAns().ToString();
            option2.text = wrong1.ToString();
            option1.text = wrong2.ToString();
        }
    }
    float CorrectAns()
    {
        switch (GlobalValues.Operation)
        {
            case '+':
                ans = num1 + num2;
                return ans;
            case '-':
                ans = num1 - num2;
                return ans;
            case '*':
                ans = num1 * num2;
                return ans;
            case '/':
                ans = num1 / num2;
                return ans;
            default:
                ans= num1 + num2;
                return ans;
        }

    }

}
