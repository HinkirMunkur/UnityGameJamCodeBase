using System;
using System.Collections;
using UnityEngine;

public static class ExtensionBase
{
    public static void ExecuteTasksOnce(this Transform taskHolder)
    {
        IExecuteable[] executeableTasks = taskHolder.GetComponents<IExecuteable>();
        
        foreach (var task in executeableTasks)
        {
            task.Run();
        }
    }
    
    public static void PlayAndCheckAnimationFinish(this MonoBehaviour monoBehaviour, Animator animator, String animationName,
        Action OnAnimationFinished)
    {
        animator.Rebind();
        animator.Play(animationName);
        monoBehaviour.StartCoroutine(CheckUntilAnimationFinish(animator, animationName, 0, OnAnimationFinished));
    }
    
    private static IEnumerator CheckUntilAnimationFinish(Animator animator, String animationName, int layerIndex, Action OnAnimationFinished)
    {
        while (!animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(animationName) ||
               animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime < 1.0f)
        {
            yield return null;
        }

        OnAnimationFinished?.Invoke();
    }
}
