using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultData
{
    public string questionText;
    public string answerText; // List of our answer 

    public ResultData(string question, string answer)
    {
        questionText = question;
        answerText = answer;
    }
}
