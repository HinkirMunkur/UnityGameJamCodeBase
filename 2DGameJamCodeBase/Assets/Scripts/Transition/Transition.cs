using System.Collections;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private CircleTransition circleTransition;
    
    [SerializeField] private GameObject[] setInactiveObjects;
    [SerializeField] private float duration;

    [SerializeField] private Animator animator;
    public bool TransitionStarted { get; set; } = false;

    private void Awake()
    {
        StartCircleTransition();
    }

    public IEnumerator StartSceneTransition()
    {
        for(int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(false);
    
        animator.Play("StartTransition");
        yield return new WaitForSecondsRealtime(duration);
        
        for(int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(true);

        TransitionStarted = false;
    }
    
    public IEnumerator EndSceneTransition(string sceneName)
    {
        TransitionStarted = true;
        
        for (int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(false);

        animator.Play("EndTransition");
        yield return new WaitForSecondsRealtime(duration);
        
        //LevelController.Instance.LoadSceneWithName(sceneName);
    }

    public void StartCircleTransition()
    {
        circleTransition.DrawBlackScreen();
        circleTransition.OpenBlackScreen();
    }
    
    public void EndCircleTransition(string sceneName)
    {
        circleTransition.DrawBlackScreen();
        circleTransition.CloseBlackScreen();
        StartCoroutine(circleTransition.Transition(1, 0.15f, 0,2.5f));

        StartCoroutine(WaitAndLoad(sceneName));
    }

    private IEnumerator WaitAndLoad(string sceneName)
    {
        yield return new WaitForSeconds(3f);
        //LevelController.Instance.LoadSceneWithName(sceneName);
    }

}
