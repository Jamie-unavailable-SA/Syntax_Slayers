using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class Word
{
    public string word;
    private int typeIndex;
    public WordDisplay wordDisplay;
    public Word(string _word, WordDisplay _wordDisplay)
    {
        word = _word;
        typeIndex = 0;
        wordDisplay = _wordDisplay;
        wordDisplay.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void typeLetter()
    {
        typeIndex++;
        wordDisplay.removeLetter();
    }

    public bool Typed()
    {
        bool WordTyped = (typeIndex >= word.Length);
        if (WordTyped)
        {
            wordDisplay.RemoveWord();
        }
        return WordTyped;
    }
}
