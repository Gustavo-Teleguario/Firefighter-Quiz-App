using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerOverview : MonoBehaviour
{

    private DataController dataController;
    public GameObject Container;
    public GameObject ContainerQuestion;
    public GameObject ContainerUserAnswers;
    //answer list
    public List<ResultData> listAnswers;
    public List<ResultData> userAnswers;

    //Prefabs
    public GameObject QuestionPrefab;
    public GameObject AnswerPrefab;
    public GameObject UserAnswerPrefab;

    //Help variables
    int counter;
    private const int questionDistance = 2800;
    private const int answerDistance = 2000;
    private const int userAnswerDistance = 1800;


    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        dataController = FindObjectOfType<DataController>();
        listAnswers = dataController.resultList;
        userAnswers = dataController.userAnswers;
        ShowAllAnswers();
        ShowUserAnswers();
    }
    public void SwipedRight()
    {
        if (counter < 4)
        {
            float x = Container.transform.position.x - answerDistance;
            float y = Container.transform.position.y;
            float z = Container.transform.position.z;
            Container.transform.position = new Vector3(x, y, z);
            SwipedQuestionsRight();
            SwipeUserAnswersRight();
            counter++;
        }
    }
    public void SwipedLeft()
    {
        if (counter != 0)
        {
            float x = Container.transform.position.x + answerDistance;
            float y = Container.transform.position.y;
            float z = Container.transform.position.z;
            Container.transform.position = new Vector3(x, y, z);
            SwipedQuestionsLeft();
            SwipeUserAnswersLeft();
            counter--;
        }
    }
    public void SwipedQuestionsRight()
    {
        float x = ContainerQuestion.transform.position.x - questionDistance;
        float y = ContainerQuestion.transform.position.y;
        float z = ContainerQuestion.transform.position.z;
        ContainerQuestion.transform.position = new Vector3(x, y, z);
    }
    public void SwipedQuestionsLeft()
    {
        float x = ContainerQuestion.transform.position.x + questionDistance;
        float y = ContainerQuestion.transform.position.y;
        float z = ContainerQuestion.transform.position.z;
        ContainerQuestion.transform.position = new Vector3(x, y, z);
    }
    public void SwipeUserAnswersRight()
    {
        float x = ContainerUserAnswers.transform.position.x - userAnswerDistance;
        float y = ContainerUserAnswers.transform.position.y;
        float z = ContainerUserAnswers.transform.position.z;
        ContainerUserAnswers.transform.position = new Vector3(x, y, z);
    }
    public void SwipeUserAnswersLeft()
    {
        float x = ContainerUserAnswers.transform.position.x + userAnswerDistance;
        float y = ContainerUserAnswers.transform.position.y;
        float z = ContainerUserAnswers.transform.position.z;
        ContainerUserAnswers.transform.position = new Vector3(x, y, z);
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
    public void ShowUserAnswers()
    {
        ResultData []answers = listAnswers.ToArray();
        ResultData[] userAns = userAnswers.ToArray();
        if(answers.Length != 0 && userAns.Length != 0)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                GameObject userAnswers = Instantiate(UserAnswerPrefab) as GameObject;
                userAnswers.transform.SetParent(ContainerUserAnswers.transform, false);
                if (answers[i].answerText == userAns[i].userAnswer)
                {
                    userAnswers.GetComponentInChildren<Text>().text = userAns[i].userAnswer;
                    userAnswers.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    userAnswers.GetComponentInChildren<Text>().text = userAns[i].userAnswer;
                    userAnswers.GetComponent<Image>().color = Color.red;
                }
            }

        }
    }
}
