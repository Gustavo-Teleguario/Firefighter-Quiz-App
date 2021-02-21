using System;

[System.Serializable]
public class UserData : IComparable<UserData>
{
    //User Atributes
    public int Range;
    public int Score;
    public string UserName;

    public UserData(string name, int score)
    {
        UserName = name;
        Score = score;
        Range = 0;
    }

    public int CompareTo(UserData other)
    {
        return other.Score.CompareTo(this.Score);
      
    }
}
