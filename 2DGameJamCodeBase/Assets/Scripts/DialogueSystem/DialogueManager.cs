using System.Collections;
using UnityEngine;
using System;

public class DialogueManager : Singletonn<DialogueManager>
{
    [Space(5)]
    [SerializeField] private DialogueHolder[] dialogueHolders;

    private DialogueHolder activeDialogueHolder = null;
    public DialogueHolder ActiveDialogueHolder => activeDialogueHolder;

    [SerializeField] private float fastWriteSpeed = 0.04f;
    private bool isCoroutineEnd = true;
    private bool fastWrite = false;

    private int dialogueIndex = 0;
    private int maxSize;

    private string word = String.Empty;
    private int wordCounter = 0;

    public Action<Dialogue> OnStartDialogueActions;
    public Func<int, RealDialogue> OnCustomDialogueActions;
    public Action OnOneDialogueEndActions;
    public Action OnEndDialogueActions;

    private bool isDialogueStarted = false;
    public bool IsDialogueStarted => isDialogueStarted;

    public bool DialogueStopGame { get; set; } = false;
    
    /// <summary>
    ///   <para> Starting to given dialogue </para>
    /// </summary>
    /// <param name="dialogue"> Dialogue Scriptable Object to be started </param>
    /// <param name="activeTextIndexInScene"> Dialogue Holder (in Dialogue Manager) to be affected </param>
    public void StartDialogue(Dialogue dialogue, int activeTextIndexInScene)
    {
        isDialogueStarted = true;
        maxSize = dialogue.sentences.Length;
        
        SetActiveTextInScene(activeTextIndexInScene);

        OnStartDialogueActions?.Invoke(dialogue);
        StartDialogueCustomActions();

        DisplayNextSentence();
    }
    
    /// <summary>
    ///   <para> Processing given dialogue.
    ///             Each DisplayNextSentence call write one sentence </para>
    /// </summary>
    public void DisplayNextSentence()
    {
        if (isCoroutineEnd == false)
        {
            fastWrite = true;
            return;
        }

        if (dialogueIndex == maxSize)
        {
            EndDialogue();
            return; 
        }

        RealDialogue realDialogue =  OnCustomDialogueActions?.Invoke(dialogueIndex);

        if (realDialogue == null)
        {
            Debug.Log("REAL DIALOGUE VALUE NULL");
        }
        else
        {
            activeDialogueHolder.audioSource.clip = realDialogue.textAudios[dialogueIndex];
        }

        GeneralDialogueColorController.Instance.ClearWordColorList(activeDialogueHolder.dialogueHolderText);
        GeneralDialogueImageController.Instance.ClearImages(activeDialogueHolder.dialogueHolderText);
        GeneralDialogueEffectController.Instance.ClearWordEffectList(activeDialogueHolder.dialogueHolderText);
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(realDialogue));
    }
    
    /// <summary>
    ///   <para> Writes proper sentence char by char </para>
    /// </summary>
    /// <param name="realDialogue"> realDialogue is an Object which is represent a Dialogue </param>
    IEnumerator TypeSentence(RealDialogue realDialogue)
    {
        if (!realDialogue.overWrite[dialogueIndex])
        {
            activeDialogueHolder.dialogueHolderText.text = String.Empty;
        }

        isCoroutineEnd = false;

        if (activeDialogueHolder.audioSource.loop == true)
        {
            activeDialogueHolder.audioSource.Play();
        }

        WaitForSecondsRealtime wfs = new WaitForSecondsRealtime(realDialogue.textWriteSpeeds[dialogueIndex]);
        WaitForSecondsRealtime wfsFast = new WaitForSecondsRealtime(fastWriteSpeed);

        word = String.Empty;
        wordCounter = 0;
        
        foreach (char letter in realDialogue.sentences[dialogueIndex])
        {
            activeDialogueHolder.dialogueHolderText.text += letter;

            if (letter != ' ')
            {
                word += letter;
                
                if (activeDialogueHolder.audioSource.loop == false)
                {
                    activeDialogueHolder.audioSource.Play();
                }
         
                if(fastWrite)
                    yield return wfsFast;
                else
                    yield return wfs;
            }
            else
            {
                GeneralDialogueColorController.Instance.TryToAddColorToWord(word, wordCounter);
                GeneralDialogueEffectController.Instance.TryToAddEffectToWord(word, wordCounter);
                
                if (GeneralDialogueImageController.Instance.TryToAddImage(word))
                {
                    activeDialogueHolder.dialogueHolderText.text += 
                        GeneralDialogueImageController.Instance.
                            AddImageAfterWord(activeDialogueHolder.dialogueHolderText, word, wordCounter);
                }
                
                wordCounter++;
                word = String.Empty;
            }

        }
        
        // Check the last word
        if (word != String.Empty)
        {
            GeneralDialogueColorController.Instance.TryToAddColorToWord(word, wordCounter);
            GeneralDialogueEffectController.Instance.TryToAddEffectToWord(word, wordCounter);
            
            if (GeneralDialogueImageController.Instance.TryToAddImage(word))
            {
                activeDialogueHolder.dialogueHolderText.text += 
                    GeneralDialogueImageController.Instance.
                        AddImageAfterWord(activeDialogueHolder.dialogueHolderText, word, wordCounter);
            } 
            
            wordCounter++;
            word = String.Empty;
        }

        dialogueIndex++;
        fastWrite = false;
        isCoroutineEnd = true;
        EndOneDialogueCustomActions();
        OnOneDialogueEndActions?.Invoke();
    }
    
    /// <summary>
    ///   <para> Writes proper sentence char by char </para>
    /// </summary>
    /// <param name="realDialogue"> realDialogue is an Object which is represent a Dialogue </param>
    private void EndDialogue()
    {
        isDialogueStarted = false;
        dialogueIndex = 0;

        OnEndDialogueActions?.Invoke();
        EndDialogueCustomActions();

    }
    
    /// <summary>
    ///   <para> Activates suitable dialogueHolder using index </para>
    /// </summary>
    /// <param name="index"> index which is represent suitable dialogueHolder in --dialogueHolders-- list  </param>
    private void SetActiveTextInScene(int index)
    {
        if (activeDialogueHolder != null)
        {
            activeDialogueHolder.UnSubsActions();
        }
        
        activeDialogueHolder = dialogueHolders[index];
        activeDialogueHolder.SubsActions();
    }
    
    /// <summary>
    ///   <para> Skips the current dialogue </para>
    /// </summary>
    public void SkipDialogue()
    {
        isCoroutineEnd = true;
        StopAllCoroutines();
        EndDialogue();
    }

    private void StartDialogueCustomActions() { }

    private void EndDialogueCustomActions() { }

    private void EndOneDialogueCustomActions() { }
    
}