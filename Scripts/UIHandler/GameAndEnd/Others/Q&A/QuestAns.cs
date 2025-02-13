using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuizController : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public List<string> options;
        public string correct_answer;
    }

    [System.Serializable]
    public class QuestionList
    {
        public List<Question> questions;
    }

    [SerializeField] private GameObject StaticObject;
    // public GameObject options;
    // public static GameObject[] options = new GameObject[4];
    public static QuizController Instance { get; private set; }

    public GameObject questionText; // Reference to the question text
    public Button[] optionButtons;      // Array of option buttons
    // public GameObject correctPopup;     // Popup for correct answer
    // public GameObject incorrectPopup;   // Popup for incorrect answer
    [SerializeField] private GameObject MoneyHandler;
    private QuestionList questionList;
    private Dictionary<int, Question> questionDictionary;
    private Question currentQuestion;

    [SerializeField] private GameObject MCQ;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadQuestions();
    }

    void LoadQuestions()
    {
        // Load the JSON file and parse it
        TextAsset jsonData = Resources.Load<TextAsset>("CyberSecQuestions"); // 'questions' is the name of the JSON file (without extension)
        if (jsonData == null)
        {
            Debug.LogError("Unable to load questions JSON file!");
            return;
        }
        
        // Parse the JSON data into the QuestionList object
        questionList = JsonUtility.FromJson<QuestionList>(jsonData.text);

        // Populate the dictionary with indexed questions
        questionDictionary = new Dictionary<int, Question>();
        for (int i = 0; i < questionList.questions.Count; i++)
        {
            questionDictionary[i] = questionList.questions[i];
        }

        Debug.Log("Loaded Questions and Ans Successfully");
        LoadRandomQuestion();
    }

    public void LoadRandomQuestion()
    {
        if (questionDictionary.Count == 0)
        {
            Debug.LogWarning("No questions available!");
            return;
        }

        // Get a random question
        int randomIndex = UnityEngine.Random.Range(0, questionDictionary.Count);
        currentQuestion = questionDictionary[randomIndex];
        DisplayQuestion(currentQuestion);
    }

    void DisplayQuestion(Question question)
    {
        // Display question text
        questionText.GetComponent<TMP_Text>().text = question.question;

        // Assign options to buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < question.options.Count)
            {
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = question.options[i];
                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => CheckAnswer(question.options[index]));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void CheckAnswer(string selectedAnswer)
    {
        if (selectedAnswer == currentQuestion.correct_answer)
        {
            // correctPopup.SetActive(true);
            GlobalObject.myGlobalObject.GetComponent<Util>().Notify("Correct Answer (Money++)");
            // MoneyHandler.GetComponent<MoneyScript>().IncrementMoney(10);
            GameData.Money += 100;
            Debug.Log("Correct");
            MCQ.SetActive(false);
        }
        else
        {
            GlobalObject.myGlobalObject.GetComponent<Util>().Notify("Incorrect Answer (Respect--)");

            Debug.Log("Incorrect");
            MCQ.SetActive(false);
            
        }
        
        LoadRandomQuestion();

    }

    public void HideMcq(){
        LoadRandomQuestion();
        MCQ.SetActive(false);
    }

        // Hide popup after a delay
        // Invoke(nameof(HidePopups), 2f);
}

