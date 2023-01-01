using System.Collections;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private ESceneTransition _startTransition;
    [SerializeField] private ESceneTransition _endTransition;
    
    [SerializeField] private CircleTransition circleTransition;
    
    [SerializeField] private GameObject[] setInactiveObjects;
    [SerializeField] private float duration;

    public bool TransitionStarted { get; set; } = false;

    private void Start()
    {
        StartSceneTransition();
    }

    public void StartSceneTransition()
    {
        StartCoroutine(ExecuteStartSceneTransition());
    }
    public void EndSceneTransition(string sceneName = null)
    {
        StartCoroutine(ExecuteEndSceneTransition(sceneName));
    }
        
    private IEnumerator ExecuteStartSceneTransition()
    {
        for(int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(false);
        
        Transition currentTransition =  TransitionController.TransitionHolderDictionary[_startTransition];
       currentTransition.ExecuteCustomStartTransition();
        
        yield return new WaitForSecondsRealtime(currentTransition.
            TransitionDuration);
        
        for(int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(true);

        TransitionStarted = false;
    }
    
    private IEnumerator ExecuteEndSceneTransition(string sceneName)
    {
        TransitionStarted = true;
        
        for (int i = 0; i < setInactiveObjects.Length; i++)
            setInactiveObjects[i].SetActive(false);

        Transition currentTransition =  TransitionController.TransitionHolderDictionary[_endTransition];
        currentTransition.ExecuteCustomEndTransition();
        
        yield return new WaitForSecondsRealtime(currentTransition.
            TransitionDuration);
       /* if(sceneName != null)
            LevelController.Instance.LoadSceneWithName(sceneName);
        */
    }

}
