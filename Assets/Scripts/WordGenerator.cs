using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WordGenerator : MonoBehaviour
{
    public static string[] wordList =
    {
        // short words (3-5 letters)
        "cat", "dog", "run", "jump", "sing", "blue", "red", "fast", "slow", "goat",
        "apple", "house", "grape", "chair", "table", "quiet", "brave", "shine", "cloud", "floor",
        "beach", "river", "ocean", "mount", "forest", "happy", "sad", "eight", "nine", "ten",

        // medium words (6-8 letters)
        "orange", "purple", "silent", "bright", "clever", "wonder", "journey", "frozen", "listen", "fantasy",
        "freedom", "glitter", "mystery", "promise", "shadows", "thinker", "twinkle", "whisper", "zebra", "advent",
        "coffee", "dazzle", "elegant", "fiction", "harmony", "imagine", "jewelry", "kindness", "liberty", "magnify",
        "nostal", "opport", "passion", "quality", "rainbow", "serenity", "training", "unique", "valuable", "winter",
        "xenial", "yellow", "zigzag", "acrobat", "balance", "captain", "delight", "emerald", "firefly", "gallery",

        // longer words (9+ letters)
        "adventure", "brilliant", "celebrate", "discovery", "elegance", "fantastic", "generously", "happiness", "illuminate", "journalist",
        "kaleidoscope", "laughter", "magnificent", "navigation", "optimistic", "parallel", "question", "revolution", "surprising", "tranquility",
        "understand", "vibrantly", "wonderful", "extraordinary", "yesterday", "amazingly", "beautiful", "complicated", "decorative", "encourage",
        "flourish", "gentleness", "historical", "important", "justified", "knowledge", "longitude", "memorable", "naturally", "overcome",
        "perfection", "quarterly", "remember", "specialist", "thankfully", "ultimate", "volunteer", "whatever", "xylophone", "youngster"
    };

    public static string GetRandomWord()
    {
        int index = Random.Range(0, wordList.Length);
        string randomWord = wordList[index];
        return randomWord;
    }
}
