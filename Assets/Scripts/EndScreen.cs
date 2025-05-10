using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        if (scoreKeeper.CalculateScore() >= 75)
        {
            finalScoreText.text = "Tebrikler gardeşim!\nBizden bi bok kazanmadın ama skorun: " 
            + scoreKeeper.CalculateScore() + "%";    
        }
        else if (scoreKeeper.CalculateScore() >= 50 && scoreKeeper.CalculateScore() < 75)
        {
           finalScoreText.text = "Tamam Tamam fazla sövmicem ama gardeşim yani cahile yakınsın ben uyarıyım\nSkorun: " 
           + scoreKeeper.CalculateScore() + "%"; 
        }
        else
        {
            finalScoreText.text = "Kara cahil seni uzak dur bu quizden ilk önce biraz bilgilen öyle gel cahil seni\nCahil ile sohbeti nanköre ilgiyi kestik :)\nSkorun:😎 " 
           + scoreKeeper.CalculateScore() + "%"; 
        }
    }
}
