using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WordSpawner : MonoBehaviour
{
    public GameObject wordObject;
    public Transform parent;
    public WordDisplay spawner()
    {
        Vector2 position = new Vector2(8, Random.Range(-3.5f, 3.5f));
        GameObject word = Instantiate(wordObject, position, Quaternion.identity, parent);
        WordDisplay wordDisplay = word.GetComponent<WordDisplay>();
        return wordDisplay;
    }
}
