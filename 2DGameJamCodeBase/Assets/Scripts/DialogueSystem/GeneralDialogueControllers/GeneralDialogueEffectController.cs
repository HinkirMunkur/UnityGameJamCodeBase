using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GeneralDialogueEffectController : Singletonn<GeneralDialogueEffectController>
{
    [SerializeField] private List<TextToEffect> textToEffectList;
    
    private Dictionary<string, TextToEffect> wordToEffectDictionary = new Dictionary<string, TextToEffect>();
    
    private List<TextIndexPair> wordToEffectList = new List<TextIndexPair>();
    
    private Coroutine changeWordColorRoutine = null;

    private TMP_Text tmpText = null;
    
    private void Awake()
    {
        foreach (var textToEffect in textToEffectList)
        {
            wordToEffectDictionary.Add(textToEffect.textKey, textToEffect);
        }
    }
    
    public void TryToAddEffectToWord(string word, int wordIndex)
    {
        if (!wordToEffectDictionary.ContainsKey(word))
        {
            return;
        }

        wordToEffectList.Add(new TextIndexPair(word, wordIndex));

        if (changeWordColorRoutine == null)
        {
            changeWordColorRoutine = StartCoroutine(ScaledChangeWordEffect());
        }
    }
    
    public void ClearWordEffectList(TMP_Text tmpText)
    {
        this.tmpText = tmpText;
        wordToEffectList.Clear();
        StopCoroutine(ScaledChangeWordEffect());
        changeWordColorRoutine = null;
    }
    
    private IEnumerator ScaledChangeWordEffect()
    {
        while (true)
        {
            if (wordToEffectList.Count == 0)
            {
                yield break;
            }

            for (int i = 0; ( wordToEffectList.Count != 0 && i < wordToEffectList.Count ); i++)
            {
                foreach (var textIndexPair in wordToEffectList)
                {
                    TextEffectsController.Instance.DoTextEffect(tmpText, textIndexPair.textIndex, 
                        wordToEffectDictionary[textIndexPair.textKey].wordEffect);
                }
            }

            yield return null;
        }
    }
    
}

[Serializable]
public class TextToEffect
{
    public string textKey;
    public ETextEffects wordEffect;
}
