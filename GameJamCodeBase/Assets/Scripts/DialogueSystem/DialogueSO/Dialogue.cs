using System.Collections.Generic;
using UnityEngine;

public abstract class Dialogue : ScriptableObject
{
    [Header("DIALOGUE ESSENTIAL VALUES")] 
    [Space(10)]
    
    [TextArea(3, 10)]
    public string[] sentences;
    
    [Space(5)]
    [Header("!!! Length should equal to Sentences Length\n" +
            "Each element represent which sentences will talk by\n" +
            "which character index !!!\n" +
            "EX:\nSentences:\n" +
            "A: Hi There !\n" +
            "B: Hi ?\n" +
            "A: Don't you know me ?\n" +
            "CharacterCount Length = 3 (0, 1, 0) like (A, B, A)")]
    
    [Space(5)]
    public int[] characterCounts;
    
    [Header("DIALOGUE DEFAULT VALUES")] 
    [Space(5)]
    [Header("!!! Each default value list length should be\n" +
            "equal --characterCounts-- (max index size + 1) then corresponding\n" +
            "index in the --characterCounts-- represent own default value !!!\n")]
    
    [Space(5)]
    public bool defOverWrites;
    [Space(5)]
    public List<float> defTextWriteSpeeds;
    public List<AudioClip> defTextAudios;
    public List<ETextEffects> defTextEffects;
    public List<Color> defDiffColor;
    [Space(5)] 

    [Header("DIALOGUE SPECIFIC VALUES")]
    [Space(10)]
    public List<TextWriteSpeedDic> textWriteSpeeds;
    public List<AudioClipDic> textAudios;
    public List<ETextEffectsDic> textEffects;
    public List<OverWriteDic> overWrites;
    public List<DiffColorDic> diffColor;
    public List<WordColorIndex> WordColorIndices;
    public List<WordEffectIdnex> WordEffectIndices;
    
}

public abstract class RealDialogue
{
    public string[] sentences;
    public float[] textWriteSpeeds;
    public AudioClip[] textAudios;
    public ETextEffects[] textEffects;
    public bool[] overWrite;
    public Color[] diffColor;
    public WordColorIndex[] wordColorIndices;
    public WordEffectIdnex[] wordEffectIndices;

    private int arraysLength;
    public virtual void Init(Dialogue dialogue)
    { 
        arraysLength = dialogue.sentences.Length;
        
        sentences = new string[arraysLength];
        textWriteSpeeds = new float[arraysLength];
        textAudios = new AudioClip[arraysLength];
        textEffects = new ETextEffects[arraysLength];
        overWrite = new bool[arraysLength];
        diffColor = new Color[arraysLength];
        wordColorIndices = new WordColorIndex[arraysLength];
        wordEffectIndices = new WordEffectIdnex[arraysLength];
    }

    public void SetText(int index, string sentence)
    {
        sentences[index] = sentence;
    }

    public void SetCustomTextWriteSpeed(int index, float customTextWriteSpeed)
    {
        textWriteSpeeds[index] = customTextWriteSpeed;
    }
    
    public void SetCustomTextAudio(int index, AudioClip customTextAudio)
    {
        textAudios[index] = customTextAudio;
    }
    
    public void SetCustomTextEffect(int index, ETextEffects customTextEffect)
    {
        textEffects[index] = customTextEffect;
    }
    
    public void SetCustomOverWrite(int index, bool customOverWrite = false)
    {
        overWrite[index] = customOverWrite;
    }
    
    public void SetCustomDiffColor(int index, Color customDiffColor)
    {
        diffColor[index] = customDiffColor;
    }

    public void SetDiffWordColorDic(int index, WordColorIndex wordColorIndex)
    {
        wordColorIndices[index] = wordColorIndex;
    }
    
    public void SetDiffWordEffectDic(int index, WordEffectIdnex wordEffectIndex)
    {
        wordEffectIndices[index] = wordEffectIndex;
    }

}