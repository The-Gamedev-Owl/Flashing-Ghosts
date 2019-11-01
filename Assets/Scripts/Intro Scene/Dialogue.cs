using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // Allows to fill informations directly in Unity Editor
public class Dialogue
{
    public string name;
    [TextArea(3, 10)] // Will display a bigger textbox in Unity Editor
    public string[] sentences;
    public Sprite dialogueBoxImage;
    public GameObject[] gameobjectToEnable;
    public AudioClip soundToPlay;
}
