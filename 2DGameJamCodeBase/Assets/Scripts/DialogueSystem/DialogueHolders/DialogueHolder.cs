using UnityEngine;
using TMPro;
using System;
using System.Collections;

public abstract class DialogueHolder : MonoBehaviour
{
    public TMP_Text dialogueHolderText;
    public AudioSource audioSource;
    public EDialogueEnd dialogueEnd;

    [SerializeField] private bool dialogueStopGame;
    public bool DialogueStopGame => dialogueStopGame;
    
    [Space(5)]
    [Header("DialogueHolder Animations (NULLABLE)")]
    [Space(5)]
    [SerializeField] private Animator dialogueHolderAnimator = null;
    [SerializeField] private string startDialogueAnimationStateName;
    [SerializeField] private string endDialogueAnimationStateName;

    private ETextEffects textEffect = ETextEffects.None;
    private WordColorIndex wordColorIndex = null;
    private WordEffectIdnex wordEffectIdnex = null;
    
    public Action HolderOnStartDialogueActions;
    public Action<RealDialogue, int> HolderOnCustomDialogueActions;
    public Action HolderOnOneDialogueEndActions;
    public Action HolderOnEndDialogueActions;
    
    protected bool StopTextEffect { get; set; } = false;
    protected bool StopChangeColor { get; set; } = false;

    private RealDialogue realDialogue = null;
    private Dialogue dialogue = null;

    private Coroutine textEffectRoutine = null;
    private Coroutine changeWordColorRoutine = null;
    private Coroutine changeWordEffectRoutine = null;
    
    /// <summary>
    ///   <para> Subscribe Dialogue Manager Actions
    ///             Called by Dialogue Manager When this --DialogueHolder-- active </para>
    /// </summary>
    public void SubsActions()
    {
        DialogueManager.Instance.OnStartDialogueActions += OnStartDialogueActions;
        DialogueManager.Instance.OnCustomDialogueActions += OnCustomDialogueActions;
        DialogueManager.Instance.OnEndDialogueActions += OnEndDialogueActions;
        DialogueManager.Instance.OnOneDialogueEndActions += OnOneDialogueEndActions;
    }
    
    /// <summary>
    ///   <para> UnSubscribe Dialogue Manager Actions
    ///             Called by Dialogue Manager When this --DialogueHolder-- inactive </para>
    /// </summary>
    public void UnSubsActions()
    {
        DialogueManager.Instance.OnStartDialogueActions -= OnStartDialogueActions;
        DialogueManager.Instance.OnCustomDialogueActions -= OnCustomDialogueActions;
        DialogueManager.Instance.OnEndDialogueActions -= OnEndDialogueActions;
        DialogueManager.Instance.OnOneDialogueEndActions -= OnOneDialogueEndActions;
    }
    
    private void OnDestroy()
    {
        DialogueManager.Instance.OnStartDialogueActions -= OnStartDialogueActions;
        DialogueManager.Instance.OnCustomDialogueActions -= OnCustomDialogueActions;
        DialogueManager.Instance.OnEndDialogueActions -= OnEndDialogueActions;
        DialogueManager.Instance.OnOneDialogueEndActions -= OnOneDialogueEndActions;
    }

    protected void SetEtextEffects(ETextEffects textEffect) => this.textEffect = textEffect;
    protected void SetWordColorIndex(WordColorIndex wordColorIndex) => this.wordColorIndex = wordColorIndex;
    protected void SetWordEffectIndex(WordEffectIdnex wordEffectIdnex) => this.wordEffectIdnex = wordEffectIdnex;
    protected abstract void InitReferences(ref RealDialogue realDialogue);
    
    protected virtual void OnStartDialogueActions(Dialogue dialogue)
    {
        if (DialogueStopGame)
        {
            DialogueManager.Instance.DialogueStopGame = true;
            Time.timeScale = 0f;
        }

        this.dialogue = dialogue;

        InitReferences(ref realDialogue);
        
        this.gameObject.SetActive(true);
        
        StopTextEffect = false;
        StopChangeColor = false;
        
        if (dialogueHolderAnimator != null)
        {
            dialogueHolderAnimator.Play(startDialogueAnimationStateName);
        }
        
        HolderOnStartDialogueActions?.Invoke();
    }

    protected virtual RealDialogue OnCustomDialogueActions(int index)
    {
        TextColorController.Instance.ChangeWholeColor(dialogueHolderText, 
            realDialogue.diffColor[index]);
        
        SetEtextEffects(realDialogue.textEffects[index]);

        SetWordEffectIndex(realDialogue.wordEffectIndices[index]);
        SetWordColorIndex(realDialogue.wordColorIndices[index]);
        
        CheckAndStartTextEffect();
        CheckAndStartWordColorEffect();

        return realDialogue;
    }

    protected virtual void OnOneDialogueEndActions()
    {

    }

    protected virtual void OnEndDialogueActions()
    {
        if (DialogueStopGame)
        {
            DialogueManager.Instance.DialogueStopGame = false;
            Time.timeScale = 1f;
        }
        
        if (dialogueHolderAnimator != null)
        {
            dialogueHolderAnimator.Play(endDialogueAnimationStateName);
        }
        
        if (dialogueEnd == EDialogueEnd.None)
        {
            this.gameObject.SetActive(false);
        }
        else if (dialogueEnd == EDialogueEnd.NextDialogue)
        {
            DialogueTrigger.Instance.TriggerDialogue();
            this.gameObject.SetActive(false);
        }
        else if (dialogueEnd == EDialogueEnd.LoadScene)
        {
       
        }
        
        StopTextEffect = true;
        StopChangeColor = true;
        
        changeWordColorRoutine = null;
        changeWordEffectRoutine = null;
        textEffectRoutine = null;
        
        HolderOnEndDialogueActions?.Invoke();
    }
    
    /// <summary>
    ///   <para> Sets the dialogue Scriptable Object's default values to realDialogue </para>
    /// <param name="index"> Corresponding sentences index </param>
    /// </summary>
    protected virtual void SetDefaultValues(int index)
    {
        realDialogue.SetText(index, dialogue.sentences[index]); //Set Texts

        realDialogue.SetCustomTextWriteSpeed(index, dialogue.defTextWriteSpeeds[dialogue.characterCounts[index]]);
        realDialogue.SetCustomTextAudio(index, dialogue.defTextAudios[dialogue.characterCounts[index]]);
        realDialogue.SetCustomTextEffect(index, dialogue.defTextEffects[dialogue.characterCounts[index]]);
        realDialogue.SetCustomOverWrite(index, dialogue.defOverWrites);
        realDialogue.SetCustomDiffColor(index, dialogue.defDiffColor[dialogue.characterCounts[index]]);
    }
    
    /// <summary>
    ///   <para> Sets the dialogue Scriptable Object's custom values to realDialogue </para>
    /// <param name="index"> Corresponding sentences index </param>
    /// </summary>
    protected virtual void ControlCustomValues(int index)
    {
        if (index < dialogue.textWriteSpeeds.Count)
        {
            realDialogue.SetCustomTextWriteSpeed(dialogue.textWriteSpeeds[index].id, 
                dialogue.textWriteSpeeds[index].textWriteSpeed);
        }
        if (index < dialogue.textAudios.Count)
        {
            realDialogue.SetCustomTextAudio(dialogue.textAudios[index].id, 
                dialogue.textAudios[index].textAudio);
        }
        if (index < dialogue.textEffects.Count)
        {
            realDialogue.SetCustomTextEffect(dialogue.textEffects[index].id, 
                dialogue.textEffects[index].textEffect);
        }
        if (index < dialogue.overWrites.Count)
        {
            realDialogue.SetCustomOverWrite(dialogue.overWrites[index].id, 
                dialogue.overWrites[index].overWrite);
        }
        if (index < dialogue.diffColor.Count)
        {   
            realDialogue.SetCustomDiffColor(dialogue.diffColor[index].id, 
                dialogue.diffColor[index].diffColor);
        }
        if (index < dialogue.WordColorIndices.Count)
        {
            realDialogue.SetDiffWordColorDic(dialogue.WordColorIndices[index].id, 
                dialogue.WordColorIndices[index]);
        }
        if (index < dialogue.WordEffectIndices.Count)
        {
            realDialogue.SetDiffWordEffectDic(dialogue.WordEffectIndices[index].id, 
                dialogue.WordEffectIndices[index]);
        }
    }

    private void CheckAndStartTextEffect()
    {
        if (textEffect != ETextEffects.None && textEffectRoutine == null)
        {
            if (DialogueStopGame)
            {
                textEffectRoutine = StartCoroutine(UnScaledDoTextEffect());
            }
            else
            {
                textEffectRoutine = StartCoroutine(ScaledDoTextEffect());
            }
        }

        if (wordEffectIdnex != null && changeWordEffectRoutine == null)
        {
            if (DialogueStopGame)
            {
                changeWordEffectRoutine = StartCoroutine(UnScaledMultipleWordDoTextEffect());
            }
            else
            {
                changeWordEffectRoutine = StartCoroutine(ScaledMultipleWordDoTextEffect());
            }
        }
        
    }

    private void CheckAndStartWordColorEffect()
    {

        if (wordColorIndex != null && changeWordColorRoutine == null)
        {
            changeWordColorRoutine = StartCoroutine(ScaledChangeWordColor());
        }
    }

    private IEnumerator ScaledDoTextEffect()
    {
        while (true)
        {
            if (StopTextEffect)
            {
                yield break;
            }

            TextEffectsController.Instance.DoTextEffect(dialogueHolderText, textEffect);

            yield return null;
        }
    }
    
    private IEnumerator ScaledMultipleWordDoTextEffect()
    {
        while (true)
        {
            if (StopTextEffect)
            {
                yield break;
            }
            
            for (int i = 0; ( wordEffectIdnex != null && i < wordEffectIdnex.diffWordEffectDics.Count ); i++)
            {
                foreach (int wordIndex in wordEffectIdnex.diffWordEffectDics[i].wordId)
                {
                    TextEffectsController.Instance.DoTextEffect(dialogueHolderText, wordIndex, 
                        wordEffectIdnex.diffWordEffectDics[i].textEffect);
                }
            }

            yield return null;
        }
    }
    

    private IEnumerator UnScaledDoTextEffect()
    {
        while (true)
        {
            if (StopTextEffect)
            {
                yield break;
            }
            
            UnScaledTextEffectController.Instance.DoTextEffect(dialogueHolderText, textEffect);

            yield return null;
        }
    }
    
    private IEnumerator UnScaledMultipleWordDoTextEffect()
    {
        while (true)
        {
            if (StopTextEffect)
            {
                yield break;
            }
            
            for (int i = 0; ( wordEffectIdnex != null && i < wordEffectIdnex.diffWordEffectDics.Count ); i++)
            {
                foreach (int wordIndex in wordEffectIdnex.diffWordEffectDics[i].wordId)
                {
                    UnScaledTextEffectController.Instance.DoTextEffect(dialogueHolderText, wordIndex, 
                        wordEffectIdnex.diffWordEffectDics[i].textEffect);
                }
            }

            yield return null;
        }
    }

    private IEnumerator ScaledChangeWordColor()
    {
        while (true)
        {
            if (StopChangeColor)
            {
                yield break;
            }

            for (int i = 0; ( wordColorIndex != null && i < wordColorIndex.diffWordColorDics.Count ); i++)
            {
                foreach (int wordIndex in wordColorIndex.diffWordColorDics[i].wordId)
                {
                    TextColorController.Instance.ChangeWordColor(dialogueHolderText, wordIndex, 
                        wordColorIndex.diffWordColorDics[i].diffColor);
                }
            }

            yield return null;
        }
    }

}
