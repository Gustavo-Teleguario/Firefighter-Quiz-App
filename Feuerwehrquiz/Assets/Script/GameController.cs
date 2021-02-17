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
    //This 2 variables need to be Deleted because we dont needet for the quiz
    // public Text scoreDisplayText;
    // public Text UserName;

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

        // playerScore = 0;
        questionIndex = 0;
        //ShowPlayerScore();
        ShowQuestion();
        //  isRoundActive = true;
    }
    public void ShowPlayerScore()
    {
        // scoreDisplayText.text = "Score: " + playerScore.ToString();
        // Debug.Log("CurrentScore: "+playerScore.ToString());#
        Debug.Log("CurrentScore: " + dataController.userData.Score);
    }
    public void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionText.text = questionData.questionText;

        //current Cuestion Number
        NumberOfCuestion();

        //REPAIR this ONE
        //Get all Answer prefabs 
        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButton = answerButtonObjectPool.GetObject();

            
                if (questionData.answers[i].answerText == "A" && questionIndex == 0)
                {
                    answerButton.GetComponent<Image>().sprite = imageBK_A;
                }

                if (questionData.answers[i].answerText == "B" && questionIndex == 0)
                {
                    answerButton.GetComponent<Image>().sprite = imageBK_B;
                }

                if (questionData.answers[i].answerText == "C" && questionIndex == 0)
                {
                    answerButton.GetComponent<Image>().sprite = imageBK_C;
                }

                if (questionData.answers[i].answerText == "D" && questionIndex == 0)
                {
                    answerButton.GetComponent<Image>().sprite = imageBK_D;
                }

             /*   if (questionData.answers[i].answerText == "F" && questionIndex == 0)
                {
                    answerButton.GetComponent<Image>().sprite = imageBK_D;
                }*/
            
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
            // scoreDisplayText.text = "Score: " + playerScore.ToString();
            //  Debug.Log("CurrentScore: " + playerScore.ToString());
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
        }
    }

    //Change Scenario
    public void GoToNextScenario()
    {
        dataController.GetNextRound();
        CheckRoundScenario();
        //Reset Variables for new Round
        SetUpRound();
        //show again
        //questionDisplay.SetActive(true);
        // changeScenario.SetActive(false);
    }

    //Return to Star and Add User into List and Save
    public void ReturnToMenu()
    {
        dataController.userData.UserName = DataController.InputField.text;
        dataController.addToList(dataController.userData.UserName, dataController.userData.Score);
        dataController.ResetCurrentRound();
        SceneManager.LoadScene("MenuScreen");
    }
    private void Update()
    {
        ShowPlayerScore();
    }

    public void NumberOfCuestion()
    {

        //Questions number and points
        int numberOfCuestion = dataController.GetNumberOfCuestion() * 3;
        currentQuestionNumber.GetComponent<Text>().text = "Frage: " + dataController.userData.Score + " / " + numberOfCuestion.ToString();
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
}
