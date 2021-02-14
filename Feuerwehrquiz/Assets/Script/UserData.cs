using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    //User Atributes

    public int Score;
    public string UserName;

    public UserData(string name, int score)
    {
        UserName = name;
        Score = score;
    }
}
