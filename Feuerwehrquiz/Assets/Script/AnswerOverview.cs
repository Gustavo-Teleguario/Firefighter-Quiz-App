using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerOverview : MonoBehaviour
{
    public const int distance = 1800;
    private DataController dataController;
    public GameObject Container;
    public GameObject ContainerQuestion;
    //answer list
    public List<ResultData> listAnswers;

    //Prefabs
    public GameObject QuestionPrefab;
    public GameObject AnswerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        dataController = FindObjectOfType<DataController>();
        listAnswers = dataController.resultList;
        ShowAllAnswers();
    }
    void Update()
    {

    }
    int counter;
    public void SwipedRight()
    {
        if (counter < 4)
        {
            float x = Container.transform.position.x - distance;
            float y = Container.transform.position.y;
            float z = Container.transform.position.z;
            Container.transform.position = new Vector3(x, y, z);
            SwipedQuestionsRight(distance);
            counter++;
        }
    }
    public void SwipedLeft()
    {
        if (counter != 0)
        {
            float x = Container.transform.position.x + distance;
            float y = Container.transform.position.y;
            float z = Container.transform.position.z;
            Container.transform.position = new Vector3(x, y, z);
            SwipedQuestionsLeft(distance);
            counter--;
        }

    }
    public void SwipedQuestionsRight(int value)
    {
        float x = ContainerQuestion.transform.position.x - value;
        float y = ContainerQuestion.transform.position.y;
        float z = ContainerQuestion.transform.position.z;
        ContainerQuestion.transform.position = new Vector3(x, y, z);
    }
    public void SwipedQuestionsLeft(int value)
    {
        float x = ContainerQuestion.transform.position.x + value;
        float y = ContainerQuestion.transform.position.y;
        float z = ContainerQuestion.transform.position.z;
        ContainerQuestion.transform.position = new Vector3(x, y, z);
    }

    public void ShowAllAnswers()
    {
        if (this.listAnswers != null)
        {
            foreach (ResultData el in listAnswers)
            {
                GameObject answer = Instantiate(AnswerPrefab) as GameObject;
                GameObject question = Instantiate(QuestionPrefab) as GameObject;
                question.transform.SetParent(ContainerQuestion.transform, true);
                answer.transform.SetParent(Container.transform, false);
                if (el.questionText == "Um welche Brandklasse handelt es sich bei diesem Brand?")
                {
                    question.GetComponentInChildren<Text>().text = "Brandklasse";
                }
                else
                {
                    question.GetComponentInChildren<Text>().text = el.questionText;
                }
                answer.GetComponentInChildren<Text>().text = el.answerText;
            }
        }
    }
}
