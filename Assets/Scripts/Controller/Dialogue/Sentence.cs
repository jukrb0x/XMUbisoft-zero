using System;
using UnityEngine;

// make sentence editable on Dialogue
[Serializable]
public class Sentence
{
    public Sprite avatar;
    [TextArea] public string sentence;
}