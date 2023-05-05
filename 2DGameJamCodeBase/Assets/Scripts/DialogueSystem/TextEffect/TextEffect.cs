using System.Collections;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] private ETextEffects customTextEffect;
    //[SerializeField] private int diffColorWordIndex = -1;
    [SerializeField] private Color textColor;
    [SerializeField] private TMP_Text tmpText;

    private void Start()
    {
        if (customTextEffect != ETextEffects.None)
        {
            StartCoroutine(ScaledDoTextEffect());
        }
    }

    private IEnumerator ScaledDoTextEffect()
    {
        while (true)
        {
            /*
            if (StopTextEffect)
            {
                yield break;
            }*/

            TextEffectsController.Instance.DoTextEffect(tmpText, customTextEffect);

            yield return null;
        }
    }
}
