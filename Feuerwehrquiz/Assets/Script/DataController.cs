using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DataController : MonoBehaviour
{
    //GameQuiz Data
    private RoundData[] allRoundData;
    public static PlayerProgress playerProgress;
    private const string gameDataFileName = "data.json";
    public List<ResultData> resultList = new List<ResultData>();

    //User Data
    public static Text InputField;
    public UserData userData;
    public List<UserData> dataList = new List<UserData>();
    public static UserData[] dataListArray;

    //Android Path
    public const string pathData = "Data/userData";
    public const string nameFileData = "UserData";



    public int scorePlayer;
    public string namePlayer;
    public void addToList(string name, int score)
    {
        scorePlayer = score;
        namePlayer = name;
        dataList.Add(new UserData(name, score));

        //Check if the User Data list Array exist or not
        if (dataListArray == null)
        {
            dataListArray = new UserData[1];
            userData = new UserData(name, score);
            dataListArray[0] = userData;
        }
        else
        {
            dataListArray = dataList.ToArray();
            Array.Sort(dataListArray);

            //This loop referenz is for a Range Result at end from quiz.
            for(int  j = 0; j  < dataListArray.Length; j++)
            { 
                dataListArray[j].Range = (j + 1);
            }
        }
        SaveUserData(dataListArray);
    }
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadGameData();
        /*****************/
        copyRigthAnswers();
        LoadPlayerProgress();
        LoadUserData();
        SceneManager.LoadScene("MenuScreen");

    }

    public void LoadUserData()
    {
        //Load Data from Android Device
        string fullPath = Application.persistentDataPath + "/" + pathData + "/" + nameFileData + ".json";
        UserData[] obj;
        if (File.Exists(fullPath))
        {
            string textJson = File.ReadAllText(fullPath);
            obj = JsonHelper.FromJson<UserData>(textJson);
            if (obj != null)
            {
                dataListArray = obj;
              //  Debug.Log("dataList " + dataListArray.Length);
            }
            else
            {
                userData = new UserData(InputField.text, userData.Score);
                dataListArray[0] = userData;
            }
        }
        else
        {
            Debug.Log("not file found");
        }
    }

    public void SaveUserData(UserData[] data)
    {
        // Save data into Device Android
        string fullPath = Application.persistentDataPath + "/" + pathData + "/";
        bool checkFolderExist = Directory.Exists(fullPath);

        if (checkFolderExist == false)
        {
            Directory.CreateDirectory(fullPath);
        }

        string json = JsonHelper.ToJson(data, true);
        File.WriteAllText(fullPath + nameFileData + ".json", json);
        Debug.Log("Save data ok. " + fullPath);
    }
    public void Update()
    {
    }
    public RoundData GetCurrentRoundData()
    {
        return allRoundData[playerProgress.currentRound];
    }
    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();
  
        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
        if (PlayerPrefs.HasKey("currentRound"))
        {
            playerProgress.currentRound = PlayerPrefs.GetInt("currentRound");
        }

    }

    private void SaveCurrentRound()
    {
        PlayerPrefs.SetInt("currentRound", playerProgress.currentRound);
    }
    public void ResetCurrentRound()
    {
        playerProgress.currentRound = 0;
        userData.Score = 0;
        SaveCurrentRound();
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
        }
    }

    public bool HasMoreRounds()
    {
        return (allRoundData.Length - 1 > playerProgress.currentRound);
    }

    public void GetNextRound()
    {
        // we increment the Round Quiz
        if (HasMoreRounds())
        {
            playerProgress.currentRound++;
            //save a current round
            SaveCurrentRound();
        }
    }

    //Load Game Data
    public void LoadGameData()
    { 
        string filePath = Path.Combine(Application.streamingAssetsPath + "/", gameDataFileName);
        string dataAsJson;
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            www.SendWebRequest();
            while (!www.isDone) ;
            dataAsJson = www.downloadHandler.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(filePath);
        }
        GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
        allRoundData = loadedData.allRoundData;
    }
    public int GetNumberOfCuestion()
    {
        return allRoundData.Length;
    }

    public void copyRigthAnswers()
    {
        for(int i = 0; i < allRoundData.Length; i++)
        {
            for (int q = 0; q < allRoundData[i].questions.Length; q++)
            {
                for (int a = 0; a < allRoundData[i].questions[q].answers.Length; a++)
                {
                    if (allRoundData[i].questions[q].answers[a].isCorrect)
                    {
                        string question = allRoundData[i].questions[q].questionText;
                        string answer = allRoundData[i].questions[q].answers[a].answerText;
                        resultList.Add(new ResultData(question,answer));
                    }
                }
            }
        }  
    }
}
