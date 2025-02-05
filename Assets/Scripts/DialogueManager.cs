using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.08f;

    public DialogueSO currentDialogue; 
    private DialogueLine currentLine;

    private bool isDialogueTyping;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        currentDialogue = dialogue;
        isDialogueActive = true;

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        StartCoroutine(DisplayNextDialogueLineWithSync());
    }

    // Coroutine dla wyświetlania linii napisów
    private IEnumerator DisplayNextDialogueLineWithSync()
    {
        if (lines.Count == 0)
        {
            currentDialogue.isComplete = true;
            EndDialogue();
            yield break;
        }

        currentLine = lines.Dequeue();

        // Czekaj, aż nadejdzie odpowiedni czas na wyświetlenie linii
        float waitTime = currentLine.startTime; // Czas startowy (w sekundach)
        yield return new WaitForSeconds(waitTime);

        dialogueArea.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        isDialogueTyping = true;
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isDialogueTyping = false;
        // Kontynuujemy wyświetlanie następnej linii po zakończeniu poprzedniej
        yield return new WaitForSeconds(1f); // Przerwa pomiędzy liniami (opcjonalna)
        StartCoroutine(DisplayNextDialogueLineWithSync());
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueArea.text = "";
    }
}
