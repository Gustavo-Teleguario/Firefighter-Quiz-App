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
    public GameObject questionDisplay;
    public GameObject changeScenario;//roundEndDisplay 
    public GameObject nextRoundDisplay;//Aca viene la siguente preguntas con sus respuestas;


    public Text currentQuestionNumber;
    public GameObject UIPanel;

    //This 2 variables need to be Deleted because we dont needet for the quiz
   // public Text scoreDisplayText;
   // public Text UserName;

    //Data variables
    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        SetUpRound();
        playerScore = 0;
   
      //  UserName.GetComponent<Text>().text = "User: " + DataController.InputField.text;
    }
    public void SetUpRound()
    {

        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;

       // playerScore = 0;
        questionIndex = 0;
        //ShowPlayerScore();
        ShowQuestion();
        isRoundActive = true;
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


        //Get all Answer prefabs 
        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButton = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButton);
            answerButton.transform.SetParent(answerButtonParent);

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
        //Este metodo es muy importante ya que lo necesitaremos para nuestro RangList
        //Este Aumenta la puntuacion del jugador si la respuesta es correcta y actualiza la pantalla
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            dataController.userData.Score = dataController.userData.Score + 1 ;
           // scoreDisplayText.text = "Score: " + playerScore.ToString();
          //  Debug.Log("CurrentScore: " + playerScore.ToString());
        }
        //Si tenemos mas preguntas, muestre la siguiente pregunta de lo contrario finalice la ronda
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
        isRoundActive = false;
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
