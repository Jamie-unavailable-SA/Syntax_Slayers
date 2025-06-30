using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public WordManager WordManager;
    public float wordDelay = 1.5f;
    private float nextWordsTime = 0f;

    private void Update()
    {
        if (Time.time >= nextWordsTime)
        {
            WordManager.addWord();
            nextWordsTime = Time.time + wordDelay;
            wordDelay *= .99f;
        }
    }
}
