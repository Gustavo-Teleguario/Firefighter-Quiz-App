using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuScreenController : MonoBehaviour
{
    //Panels
    public GameObject StartPanel;
    public GameObject PanelName;
    public GameObject PanelList;

    //DataController
    public Text InputField;
    public DataController dataController;

    //Show List of Users
    public GameObject Container;
    //Text Prefabs for List
    public UserData[] listUsers;

    public GameObject prefNumber;
    public GameObject prefabText;
    public GameObject prefabScore;
   

    void Start()
    {
        DataController.InputField = this.InputField;
        this.listUsers = DataController.dataListArray;
        SortMyArray(this.listUsers);
        showListPlayers();
    }
    public void SortMyArray(UserData[] listUsers)
    {
        if(listUsers.Length != 0)
        {
           Array.Sort(listUsers);
        }
       
    }

    public void StartPanelName()
    {
        if (PanelName != null)
        {
            PanelName.SetActive(true);
            StartPanel.SetActive(false);
        }
    }

    public void StartPanelList()
    {
        if (PanelList != null)
        {
            PanelList.SetActive(true);
            StartPanel.SetActive(false);
        }
    }

    public void BackToStart()
    {
        if (StartPanel != null)
        {
            StartPanel.SetActive(true);
        }
        PanelName.SetActive(false);
        PanelList.SetActive(false);

    }

    //Start first Round Scenario
    public void StartQuiz()
    {
        SceneManager.LoadScene("Round");
    }

    //This Container is for RangList
    public void showListPlayers()
    {
        if (this.listUsers != null)
        {
            for (int i = 0; i < listUsers.Length; i++)
            { 
                GameObject textNumber = Instantiate(prefNumber) as GameObject;
                GameObject text = Instantiate(prefabText) as GameObject;
                GameObject textScore = Instantiate(prefabScore) as GameObject;
                textNumber.transform.SetParent(Container.transform, false);
                text.transform.SetParent(Container.transform, false);
                textScore.transform.SetParent(Container.transform, false);
                textNumber.GetComponentInChildren<Text>().text = (i + 1).ToString();
                text.GetComponentInChildren<Text>().text = listUsers[i].UserName;
                textScore.GetComponentInChildren<Text>().text = listUsers[i].Score.ToString();
               
            }
        }
    }
}
