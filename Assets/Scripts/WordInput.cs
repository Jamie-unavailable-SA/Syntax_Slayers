using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WordInput : MonoBehaviour
{
    public WordManager WordManager;
    private void Update()
    {
        foreach (char letter in Input.inputString)
        {
            WordManager.Letter(letter);
        }
    }
}
