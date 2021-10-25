using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;
    [SerializeField] int letterPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public static DialogueManager Instance{ get; private set; }

    public void Awake()
    {
        Instance = this;        
    }

    Dialogue dialogue;
    Action onDialogFinished;
    int currentLine = 0;
    bool isTyping;

    public bool  IsShowing { get; private set;}    

    public IEnumerator ShowDialogue(Dialogue dialogue, Action onFinished=null)
    {
        //Waits for 1 frame to prevent wrong outputs
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();
        IsShowing = true;
        this.dialogue = dialogue;
        onDialogFinished = onFinished;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    //To prevent the movement of player while conversations with NPCs
    public void HandleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else 
            {
                currentLine = 0;
                IsShowing = false;
                dialogueBox.SetActive(false);
                onDialogFinished?.Invoke();
                OnCloseDialog?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTyping = false;
    }
}