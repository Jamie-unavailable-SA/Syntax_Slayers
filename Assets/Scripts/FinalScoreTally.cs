using UnityEngine;
using UnityEngine.UI;                      // <- leave this if you’re using TextMeshPro

public class FinalScoreTally : MonoBehaviour
{
    [SerializeField] private Text finalScoreLabel;

    private void OnEnable()       // runs automatically when the panel activates
    {
        if (finalScoreLabel == null)
        {
            Debug.LogError("FinalScoreTally: No Text reference set!", this);
            return;
        }

        finalScoreLabel.text = $"Final score: {WordManager.score}";
    }
}
