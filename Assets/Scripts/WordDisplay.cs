using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class WordDisplay : MonoBehaviour
{
    public Text text;
    public float speed = 1f;
    public void SetWord(string word)
    {
        text.text = word;
    }

    public void removeLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.orange;
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }
}
