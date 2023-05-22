using System.Collections;
using UnityEngine;
using System;

public static class CoroutineExtensionBase
{
    public static void WaitForSeconds(this MonoBehaviour monoBehaviour, float duration, Action OnRoutineFinished)
    {
        monoBehaviour.StartCoroutine(WaitForSecondsRoutine(duration, OnRoutineFinished));
    }

    private static IEnumerator WaitForSecondsRoutine(float duration, Action OnRoutineFinished)
    {
        yield return new WaitForSeconds(duration);
        OnRoutineFinished?.Invoke();
    }
}
