using UnityEngine;
using TMPro;

public class TextColorController : Singletonn<TextColorController>
{
    public void ChangeWordColor(TMP_Text dialogueHolderText, int wordColorIndex, Color color)
    {
        var textInfo = dialogueHolderText.textInfo;

        if(wordColorIndex <= textInfo.wordCount-1)
        {
            for (int i = textInfo.wordInfo[wordColorIndex].firstCharacterIndex; 
                 i <= textInfo.wordInfo[wordColorIndex].lastCharacterIndex; i++)
            {
                var charInfo = textInfo.characterInfo[i];

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                Color32[] vertexColors = textInfo.meshInfo[charInfo.materialReferenceIndex].colors32;

                vertexColors[vertexIndex + 0] = color;
                vertexColors[vertexIndex + 1] = color;
                vertexColors[vertexIndex + 2] = color;
                vertexColors[vertexIndex + 3] = color;
            }
        }

        dialogueHolderText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

    }
    
    public void ChangeWholeColor(TMP_Text dialogueHolderText, Color color)
    {
        dialogueHolderText.color = color;
    }
    
}
