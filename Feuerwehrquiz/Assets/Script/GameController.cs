using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    // UI
    public Text questionText;
    public ObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public Transform answerButtonParentTwo;
    public GameObject questionDisplay;
    public GameObject changeScenario;//roundEndDisplay 
    public GameObject nextRoundDisplay;


    public Text currentQuestionNumber;
    public GameObject UIPanel;
    //Sprites Answers
    public Sprite imageBK_A;
    public Sprite imageBK_B;
    public Sprite imageBK_C;
    public Sprite imageBK_D;
    public Sprite imageBK_F;
    public Sprite spriteTest;

    //End display 
    public Text correctAnswers;

    //Data variables
    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    // private bool isRoundActive;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        SetUpRound();
        playerScore = 0;

    }
    public void SetUpRound()
    {
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        questionIndex = 0;
        ShowQuestion();

    }
    public void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionText.text = questionData.questionText;
        NumberOfCuestion();

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButton = answerButtonObjectPool.GetObject();
            if (questionData.answers[i].answerText == "A")
            {
                answerButton.GetComponent<Image>().sprite = imageBK_A;
                answerButton.GetComponentInChildren<Text>().color = Color.clear;
            }

            if (questionData.answers[i].answerText == "B")
            {
                answerButton.GetComponent<Image>().sprite = imageBK_B;
                answerButton.GetComponentInChildren<Text>().color = Color.clear;
            }

            if (questionData.answers[i].answerText == "C")
            {
                answerButton.GetComponent<Image>().sprite = imageBK_C;
                answerButton.GetComponentInChildren<Text>().color = Color.clear;
            }

            if (questionData.answers[i].answerText == "D")
            {
                answerButton.GetComponent<Image>().sprite = imageBK_D;
                answerButton.GetComponentInChildren<Text>().color = Color.clear;
            }

            if (questionData.answers[i].answerText == "F")
            {
                answerButton.GetComponent<Image>().sprite = imageBK_F;
                answerButton.GetComponentInChildren<Text>().color = Color.clear;
            }

            string answerText = questionData.answers[i].answerText;
            //Round
            if (answerText == "Schaum" || answerText == "CO_2 Löscher" || answerText == "Wasser"
                || answerText == "Sand" || answerText == "ABC-Löschpulver" || answerText == "Wasser (Sprühlstrahl)"
                || answerText == "Fettebrand Löschmittel" || answerText == "Fettbrand Löschmittel" || answerText == "ABC Pulverlöscher"
                || answerText == "C02_Löscher")
            {
                answerButton.GetComponent<Image>().sprite = spriteTest;
                answerButton.GetComponentInChildren<Text>().color = Color.black;

            }

            answerButtonGameObjects.Add(answerButton);
            if (questionData.answers.Length != 4)
            {
                answerButton.transform.SetParent(answerButtonParent);
            }
            else
            {
                answerButton.transform.SetParent(answerButtonParentTwo);
            }
            //Imagen Setup
            AnswerPrefab answerPrefab = answerButton.GetComponent<AnswerPrefab>();
            answerPrefab.Setup(questionData.answers[i]);
        }

    }
    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            dataController.userData.Score = dataController.userData.Score + 1;
        }
        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }
    public void EndRound()
    {
        //Set Round an turn off the Display question and activate the Change Scenario Panel
        // isRoundActive = false;
        dataController.SubmitNewPlayerScore(playerScore);

        questionDisplay.SetActive(false);
        UIPanel.SetActive(false);
        changeScenario.SetActive(true);

        if (dataController.HasMoreRounds())
        {
            nextRoundDisplay.SetActive(true);
        }
        else
        {
            nextRoundDisplay.SetActive(false);
            PlayerAddedIntoListData();
        }
    }

    //Change Scenario
    public void GoToNextScenario()
    {
        dataController.GetNextRound();
        CheckRoundScenario();
        //Reset Variables for new Round
        SetUpRound();

    }

    //Return to Star and Add User into List and Save
    public void ReturnToMenu()
    {
        dataController.ResetCurrentRound();
        SceneManager.LoadScene("MenuScreen");
    }

    private void Update()
    {
        EndResult();
    }

    public void PlayerAddedIntoListData()
    {
        dataController.userData.UserName = DataController.InputField.text;
        dataController.addToList(dataController.userData.UserName, dataController.userData.Score);
    }

    public void NumberOfCuestion()
    {
        //Questions number and points
        int numberOfCuestion = dataController.GetNumberOfCuestion() * 3;
        currentQuestionNumber.GetComponent<Text>().text = "Frage: " + dataController.userData.Score + " / " + numberOfCuestion.ToString();

    }

    //Show End Result
    public void EndResult()
    {
        if (!dataController.HasMoreRounds())
        {
            int numberOfCuestion = dataController.GetNumberOfCuestion() * 3;
            correctAnswers.GetComponent<Text>().text = "Sie haben " + dataController.userData.Score + " von " + numberOfCuestion
                                                  + " Fragen richtig beantwortet. Sie belegen damit Platz "+ RangePlace()+ " auf der Rangliste";
        }

    }

    //show Cuestion
    public void ShowPanelCuestion()
    {
        // ReturnToMenu();
        if (nextRoundDisplay != null && !nextRoundDisplay.activeSelf)
        {
            nextRoundDisplay.SetActive(true);
        }
        else
        {
            nextRoundDisplay.SetActive(false);
        }

    }

    public void CheckRoundScenario()
    {
        //Switch into Scenearios
        if (DataController.playerProgress.currentRound > 0)
        {
            SceneManager.LoadScene("Round 1");
        }
        if (DataController.playerProgress.currentRound > 1)
        {
            SceneManager.LoadScene("Round 2");
        }
        if (DataController.playerProgress.currentRound > 2)
        {
            SceneManager.LoadScene("Round 3");
        }
        if (DataController.playerProgress.currentRound > 3)
        {
            SceneManager.LoadScene("Round 4");
        }

    }
    //Return the current range player
    public int RangePlace()
    {
        for (int i = 0; i < DataController.dataListArray.Length; i++)
        { 
            if(dataController.userData.Score == DataController.dataListArray[i].Score)
            {
                return DataController.dataListArray[i].Range;
            }
        }
        return 0;
    }
}
