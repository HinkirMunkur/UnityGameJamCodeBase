using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralDialogueColorController : Singletonn<GeneralDialogueColorController>
{
    [SerializeField] private List<TextToColor> textToColorList;

    private Dictionary<string, TextToColor> wordToColorDictionary = new Dictionary<string, TextToColor>();

    private List<TextIndexPair> wordToColorList = new List<TextIndexPair>();

    private Coroutine changeWordColorRoutine = null;

    private TMP_Text tmpText = null;
    
    private void Awake()
    {
        foreach (var textToColor in textToColorList)
        {
            wordToColorDictionary.Add(textToColor.textKey, textToColor);
        }
    }
    
    public void TryToAddColorToWord(string word, int wordIndex)
    {
        if (!wordToColorDictionary.ContainsKey(word))
        {
            return;
        }

        wordToColorList.Add(new TextIndexPair(word, wordIndex));

        if (changeWordColorRoutine == null)
        {
            changeWordColorRoutine = StartCoroutine(ScaledChangeWordColor());
        }
    }

    public void ClearWordColorList(TMP_Text tmpText)
    {
        this.tmpText = tmpText;
        wordToColorList.Clear();
        StopCoroutine(ScaledChangeWordColor());
        changeWordColorRoutine = null;
    }
    
    private IEnumerator ScaledChangeWordColor()
    {
        while (true)
        {
            if (wordToColorList.Count == 0)
            {
                yield break;
            }

            for (int i = 0; ( wordToColorList.Count != 0 && i < wordToColorList.Count ); i++)
            {
                foreach (var textIndexPair in wordToColorList)
                {
                    TextColorController.Instance.ChangeWordColor(tmpText, textIndexPair.textIndex, 
                        wordToColorDictionary[textIndexPair.textKey].wordColor);
                }
            }

            yield return null;
        }
    }

}

[Serializable]
public class TextToColor
{
    public string textKey;
    public Color wordColor;
}

[Serializable]
public class TextIndexPair
{
    public string textKey;
    public int textIndex;

    public TextIndexPair(string textKey, int textIndex)
    {
        this.textKey = textKey;
        this.textIndex = textIndex;
    }
}
