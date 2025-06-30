using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WordManager : MonoBehaviour
{
    public List<Word> words;
    public bool hasActiveWord;
    public Word activeWord;
    public WordSpawner spawner;
    public void Start()
    {
        addWord();
        addWord();
        addWord();
        addWord();

    }

    public void addWord()
    {
        Word word = new Word(WordGenerator.GetRandomWord(), spawner.spawner());
        words.Add(word);
        Debug.Log(word.word);
    }

    public void Letter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.typeLetter();
            }
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.typeLetter();
                    break;
                }
            }
        }

        if (hasActiveWord && activeWord.Typed())
        {
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }
}
