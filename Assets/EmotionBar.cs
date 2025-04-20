using UnityEngine;
using UnityEngine.UI;

public class EmotionBar : MonoBehaviour
{
    public Slider pleasureBar;
    public Slider anxietyBar;

    private float pleasure = 0f;
    private float anxiety = 0f;

    void Start()
    {
        UpdateBars();
    }

    public void ChangeEmotion(int pleasureChange, int anxietyChange)
    {
        pleasure = Mathf.Max(0, pleasure + pleasureChange);
        anxiety += anxietyChange;

        pleasure = Mathf.Clamp(pleasure, 0, 100);
        anxiety = Mathf.Clamp(anxiety, 0, 100);

        UpdateBars();
    }

    void UpdateBars()
    {
        pleasureBar.value = pleasure;
        anxietyBar.value = anxiety;
    }
}
