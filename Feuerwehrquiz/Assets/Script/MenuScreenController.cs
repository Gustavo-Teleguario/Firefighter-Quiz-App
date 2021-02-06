using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenController : MonoBehaviour
{
    //Panels
    public GameObject StartPanel;
    public GameObject PanelName;
    public GameObject PanelList;

    //start the next Scene
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

    public void StartQuiz()
    {
        Debug.Log("Game is Started");
    }
}
