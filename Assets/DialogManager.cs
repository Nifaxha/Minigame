using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class QuestionData
{
    public string question;
    public string[] answers = new string[2]; // Hanya 2 jawaban
    public int correctIndex; // 0 atau 1
    public string correctFeedback;
    public string wrongFeedback;

}

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button answer1;
    public Button answer2;
    public Button nextButton; // tombol untuk lanjut ke pertanyaan berikutnya
    public TextMeshProUGUI feedbackText; // Tambahkan ini untuk menampilkan feedback
    public GameObject feedbackObject; // Objek tempat TMP feedbackText berada


    private EmotionBar emotionBar;

    public QuestionData[] questions;
    private int currentQuestionIndex = 0;

    void Start()
    {
        emotionBar = GetComponent<EmotionBar>();
        SetupButtonListeners();
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(NextQuestion);
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            questionText.text = "Terima kasih telah bermain!";
            answer1.gameObject.SetActive(false);
            answer2.gameObject.SetActive(false);
            feedbackObject.SetActive(false); // Sembunyikan saat akhir
            return;
        }

        var q = questions[currentQuestionIndex];
        questionText.text = q.question;

        answer1.GetComponentInChildren<TextMeshProUGUI>().text = q.answers[0];
        answer2.GetComponentInChildren<TextMeshProUGUI>().text = q.answers[1];

        answer1.interactable = true;
        answer2.interactable = true;

        feedbackObject.SetActive(false); // Sembunyikan saat pertanyaan dimuat
    }


    void SetupButtonListeners()
    {
        answer1.onClick.AddListener(() => HandleAnswer(0));
        answer2.onClick.AddListener(() => HandleAnswer(1));
    }

    void HandleAnswer(int chosenIndex)
    {
        var q = questions[currentQuestionIndex];

        answer1.interactable = false;
        answer2.interactable = false;

        // Sembunyikan pertanyaan dan jawaban
        questionText.gameObject.SetActive(false);
        answer1.gameObject.SetActive(false);
        answer2.gameObject.SetActive(false);

        feedbackObject.SetActive(true);

        if (chosenIndex == q.correctIndex)
        {
            int randPleasure = Random.Range(5, 16);
            feedbackText.text = q.correctFeedback;
            AnswerSelected(randPleasure, 0);
        }
        else
        {
            int randAnxiety = Random.Range(5, 16);
            int randPleasure = -Random.Range(5, 16);
            feedbackText.text = q.wrongFeedback;
            AnswerSelected(randPleasure, randAnxiety);
        }

        // Tampilkan tombol lanjut
        nextButton.gameObject.SetActive(true);
    }


    void NextQuestion()
    {
        currentQuestionIndex++;
        feedbackObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        questionText.gameObject.SetActive(true);
        answer1.gameObject.SetActive(true);
        answer2.gameObject.SetActive(true);

        LoadQuestion();
    }


    void AnswerSelected(int pleasureChange, int anxietyChange)
    {
        emotionBar.ChangeEmotion(pleasureChange, anxietyChange);
    }
}
