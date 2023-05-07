using UnityEngine;
using TMPro;

public class TextEffectsController : Singletonn<TextEffectsController>
{
    private const int TMP_PRO_VERTICES = 4;

    private int neg = 1;
    private float increaseAmount = 0;
    
    public void DoTextEffect(TMP_Text dialogueHolderText, ETextEffects textEffect)
    {
        dialogueHolderText.ForceMeshUpdate();

        var textInfo = dialogueHolderText.textInfo;
        
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            if (textEffect == ETextEffects.Wiggle)
                WiggleTextEffect(verts, charInfo);
            else if (textEffect == ETextEffects.Vibration)
                VibrationTextEffect(verts, charInfo);
            else if (textEffect == ETextEffects.VibrationV2)
                VibrationTextEffectV2(verts, charInfo);
            else if (textEffect == ETextEffects.Rage)
                RageTextEffect(verts, charInfo);
            else if (textEffect == ETextEffects.Glitch)
                GlitchTextEffect(verts, charInfo);
            else if (textEffect == ETextEffects.LightGlitch)
                LightGlitchTextEffect(verts, charInfo);
        }

        ChangeWorkingCopy(dialogueHolderText, textInfo);
    }

    public void DoTextEffect(TMP_Text dialogueHolderText, int wordColorIndex, ETextEffects textEffect)
    {
        //dialogueHolderText.ForceMeshUpdate();

        var textInfo = dialogueHolderText.textInfo;

        if (wordColorIndex <= textInfo.wordCount - 1)
        {
            for (int i = textInfo.wordInfo[wordColorIndex].firstCharacterIndex;
                 i <= textInfo.wordInfo[wordColorIndex].lastCharacterIndex; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                
                if (!charInfo.isVisible)
                    continue;
                
                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
                
                if (textEffect == ETextEffects.Wiggle)
                    WiggleTextEffect(verts, charInfo);
                else if (textEffect == ETextEffects.Vibration)
                    VibrationTextEffect(verts, charInfo);
                else if (textEffect == ETextEffects.VibrationV2)
                    VibrationTextEffectV2(verts, charInfo);
                else if (textEffect == ETextEffects.Rage)
                    RageTextEffect(verts, charInfo);
                else if (textEffect == ETextEffects.Glitch)
                    GlitchTextEffect(verts, charInfo);
                else if (textEffect == ETextEffects.LightGlitch)
                    LightGlitchTextEffect(verts, charInfo);
            }
            
            ChangeWorkingCopy(dialogueHolderText, textInfo);
        }

    }

    private void ChangeWorkingCopy(TMP_Text dialogueHolderText, TMP_TextInfo textInfo)
    {
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];

            if (meshInfo.mesh != null)
            {
                meshInfo.mesh.vertices = meshInfo.vertices;
                dialogueHolderText.UpdateGeometry(meshInfo.mesh, i);
            }
        }
    }

    private void WiggleTextEffect(Vector3[] verts, TMP_CharacterInfo charInfo)
    {
        neg *= -1;

        for (int j = 0; j < TMP_PRO_VERTICES; j++)
        {
            var orig = verts[charInfo.vertexIndex + j];
            verts[charInfo.vertexIndex + j] = orig + new Vector3(Mathf.Cos(Time.time * 2 + orig.x * 0.7f) * 3 * neg, Mathf.Sin(Time.time * 8) * 0.6f * neg, 0);
        }
    }

    private void VibrationTextEffect(Vector3[] verts, TMP_CharacterInfo charInfo)
    {
        neg *= -1;

        for (int j = 0; j < TMP_PRO_VERTICES; j++)
        {
            var orig = verts[charInfo.vertexIndex + j];
            verts[charInfo.vertexIndex + j] = orig + new Vector3(Mathf.Sin(Time.time * 80) * 0.6f * neg, Mathf.Sin(Time.time * 60) * 0.6f * neg, 0);
        }
    }

    private void VibrationTextEffectV2(Vector3[] verts, TMP_CharacterInfo charInfo)
    {
        neg *= -1;

        for (int j = 0; j < TMP_PRO_VERTICES; j++)
        {
            var orig = verts[charInfo.vertexIndex + j];
            verts[charInfo.vertexIndex + j] = orig + new Vector3(Mathf.Sin(Time.time * 2 + orig.x * 2) * 3 * neg, Mathf.Sin(Time.time * 3 + orig.x * 1.5f) * 4 * neg, 0);
        }
    }

    private void RageTextEffect(Vector3[] verts, TMP_CharacterInfo charInfo)
    {
        neg *= -1;

        if (increaseAmount < 5)
        {
            increaseAmount += Time.deltaTime / 50;
        }

        for (int j = 0; j < TMP_PRO_VERTICES; j++)
        {
            var orig = verts[charInfo.vertexIndex + j];
            verts[charInfo.vertexIndex + j] = orig + new Vector3(Mathf.Tan(Time.time * 1 + orig.x * increaseAmount) * increaseAmount * neg,
                Mathf.Tan(Time.time * 1 + orig.x * increaseAmount) * increaseAmount * neg, 0);
        }
    }

    private void GlitchTextEffect(Vector3[] verts, TMP_CharacterInfo charInfo)
    {
        neg *= -1;

        for (int j = 0; j < TMP_PRO_VERTICES; j++)
        {
            var orig = verts[charInfo.vertexIndex + j];
            verts[charInfo.vertexIndex + j] = orig + new Vector3(Mathf.Tan(Time.time + orig.x * 50) * 0.1f * neg, Mathf.Tan(Time.time + orig.x * 50) * 0.1f * neg, 0);
        }
    }

    private void LightGlitchTextEffect(Vector3[] verts, TMP_CharacterInfo charInfo)
    {
        neg *= -1;

        for (int j = 0; j < TMP_PRO_VERTICES; j++)
        {
            var orig = verts[charInfo.vertexIndex + j];
            verts[charInfo.vertexIndex + j] = orig + new Vector3(Mathf.Tan(Time.time * 2 + orig.x) * 0.01f * neg, Mathf.Tan(Time.time * 2 + orig.x) * 0.01f * neg, 0);
        }
    }


}
