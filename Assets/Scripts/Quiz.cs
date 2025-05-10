using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswer;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswer;
    [SerializeField] Sprite correctOne;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;
    
    void Awake()
    {
        timer = FindAnyObjectByType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update() 
    {
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            if (progressBar.maxValue == progressBar.value)
            {
                isComplete = true;
                return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelect(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: "+ scoreKeeper.CalculateScore()+ "%";
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        
        if (index == currentQuestion.GetCorrecAnswer())
        {
            questionText.text = "Helal Lan!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctOne;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswer = currentQuestion.GetCorrecAnswer();
            string CA = currentQuestion.GetAnswer(correctAnswer);
            questionText.text = "Yanlış cahil doğru cevap:\n "+CA;
            buttonImage = answerButtons[correctAnswer].GetComponent<Image>();
            buttonImage.sprite = correctOne;
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultSprite();
            GetRandomQuestion();
            progressBar.value++;
            DisplayQuestion();
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultSprite()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswer;
        }
    }
}
