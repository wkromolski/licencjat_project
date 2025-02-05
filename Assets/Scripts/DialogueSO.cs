using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class DialogueSO : ScriptableObject
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    public bool isComplete = false; 
}

[System.Serializable]
public class DialogueLine
{
    public string line;
    public float startTime;
}