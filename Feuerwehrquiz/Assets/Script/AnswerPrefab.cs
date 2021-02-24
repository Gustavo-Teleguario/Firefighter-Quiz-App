using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerPrefab : MonoBehaviour
{
    public Text answerText;
    private GameController gameController;
    private AnswerData answerData;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void Setup(AnswerData data)//pass in answer data and set up for display
    {
        answerData = data;
        answerText.text = answerData.answerText;    
    }
   public void HandleClick()
    {
        gameController.AnswerButtonClicked(answerData.isCorrect, answerData.answerText);   
    }
}
