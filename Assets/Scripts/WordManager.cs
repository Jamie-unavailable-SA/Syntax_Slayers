using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WordManager : MonoBehaviour
{
    public List<Word> words;
    public bool hasActiveWord;
    public bool isActive;
    public Word activeWord;
    public WordSpawner spawner;

    [Header("bullet effects")]
    public GameObject prefab;
    public Transform player;
    public GameObject bullet;


    public void addWord()
    {
        Word word = new Word(WordGenerator.GetRandomWord(), spawner.spawner());
        words.Add(word);
        Debug.Log(word.word);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (letterKey())
            {
                isActive = true;
            }
        }
        else
        {
            isActive = false;
        }
        if (isActive)
        {
            prefab = Instantiate(bullet, player.position, Quaternion.identity);
            Destroy(bullet, 1f);
        }
    }

    bool letterKey()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                return true;
            }
        }
        return false;
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
            Destroy(prefab);
        }
    }
}
