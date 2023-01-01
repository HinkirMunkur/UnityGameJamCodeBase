using System.Collections;
using UnityEngine;

public class Transition2 : MonoBehaviour
{
    [SerializeField] private GameObject[] setInactiveObjects;
    [SerializeField] private float duration;

    [SerializeField] private Animator animator;
    public bool TransitionStarted { get; set; } = false;

    public IEnumerator StartTransition()
    {
        for(int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(false);
    
        animator.Play("StartTransition");
        yield return new WaitForSecondsRealtime(duration);
        
        for(int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(true);

        TransitionStarted = false;
    }
    
    public IEnumerator EndTransition(string sceneName)
    {
        TransitionStarted = true;
        
        for (int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(false);

        animator.Play("EndTransition");
        yield return new WaitForSecondsRealtime(duration);
        
        //LevelController.Instance.LoadSceneWithName(sceneName);
    }
}
