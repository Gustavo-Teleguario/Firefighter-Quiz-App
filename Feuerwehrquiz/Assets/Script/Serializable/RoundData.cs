using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData 
{
    public int pointsAddedForCorrectAnswer; // this will be needed for our Counter poinst for SaveData
    public QuestionData[] questions; //list of questions for the Round;
}
