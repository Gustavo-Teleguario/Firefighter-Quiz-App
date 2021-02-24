using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultData
{
    public string questionText;
    public string answerText; // List of our answer 
    public string userAnswer;

    public ResultData(string question, string answer, string userAns)
    {
        questionText = question;
        answerText = answer;
        userAnswer = userAns;
    }
}
