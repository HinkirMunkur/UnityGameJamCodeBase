using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralDialogueImageController : Singletonn<GeneralDialogueImageController>
{
    [SerializeField] private List<TextToSprite> textToSpriteList;

    private Dictionary<string, TextToSprite> wordToSpriteDictionary = new Dictionary<string, TextToSprite>();

    // Contains Ascending Indices
    public List<int> WordImageIndexList { get; set; } = new List<int>();

    private void Awake()
    {
        foreach (var textToSpriteItem in textToSpriteList)
        {
            wordToSpriteDictionary.Add(textToSpriteItem.textKey, textToSpriteItem);
        }
    }

    public bool TryToAddImage(string word) => wordToSpriteDictionary.ContainsKey(word);

    /// <summary>
    ///   <para> Matching words with images by looking for predefined list </para>
    /// </summary>
    /// <param name="text"> Written text which has a spriteAsset </param>
    /// <param name="word"> sentence's word check for is this a element of  --wordToSpriteDictionary-- </param>
    /// <param name="wordIndex"> wordIndex is a index which is matching to --TMP_SpriteAsset's-- desired image </param>
    public String AddImageAfterWord(TMP_Text text, string word, int wordIndex) // Call Only After TryToAddImage
    {
        WordImageIndexList.Add(wordIndex);
        
        text.spriteAsset = wordToSpriteDictionary[word].tmpSprite;
        
        return ImageTextFormater(wordToSpriteDictionary[word].spriteAssetId);
    }
    
    /// <summary>
    ///   <para> Clears creating pre images </para>
    /// </summary>
    /// <param name="text"> Written text which has a spriteAsset </param>
    public void ClearImages(TMP_Text text)
    {
        text.spriteAsset = null;

        TMP_SubMeshUI tmpSubMeshUI = text.GetComponentInChildren<TMP_SubMeshUI>();

        if (tmpSubMeshUI != null)
        {
            Destroy(text.GetComponentInChildren<TMP_SubMeshUI>().gameObject);
        }
        
        WordImageIndexList.Clear();
    }

    private string ImageTextFormater(int id)
    {
        return $"<sprite={id}> ";
    }
    
}

[Serializable]
public class TextToSprite
{
    public string textKey;
    public TMP_SpriteAsset tmpSprite;
    public int spriteAssetId;
}
