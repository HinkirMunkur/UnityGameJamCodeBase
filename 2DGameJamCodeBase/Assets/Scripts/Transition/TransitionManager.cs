using System.Collections;
using EasyButtons;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : Singletonn<TransitionManager>
{
    [SerializeField] private ESceneTransition _startTransitionType;
    [SerializeField] private ESceneTransition _endTransitionType;
    
    [SerializeField] private GameObject[] setInactiveObjects;
    [SerializeField] private float duration;

    [SerializeField] private Transform transitionTypeHolder;
    [SerializeField] private Canvas blackScreenCanvas;

    [SerializeField] private Transition startTransition = null;
    [SerializeField] private Transition endTransition = null;

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
        
        startTransition.ExecuteCustomStartTransition(duration);
        
        yield return new WaitForSecondsRealtime(startTransition.
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
        
        endTransition.ExecuteCustomEndTransition(duration);
        
        yield return new WaitForSecondsRealtime(endTransition.
            TransitionDuration);
        
       /*
        if(sceneName != null)
            LevelController.Instance.LoadSceneWithName(sceneName);
        */
    }
    
#if UNITY_EDITOR
    
    [Button]
    private void CreateTransitionObjects()
    {
        SetDefault();
        
        foreach (var val in TransitionRefereneces.Instance.ESceneTransitionToTransitionList)
        {
            if (val.ESceneTransition == _startTransitionType)
            {
                startTransition = Instantiate(val.Transition, transitionTypeHolder);
                GameObject GO = Instantiate(val.BlackScreen,blackScreenCanvas.transform);
                
                startTransition.SetTransitionReferences(GO.GetComponent<Image>());
                
                UnityEditor.EditorUtility.SetDirty(this);
                
                if (_startTransitionType == _endTransitionType)
                {
                    endTransition = startTransition;
                    break;
                }
            }
            else if(val.ESceneTransition == _endTransitionType)
            {
                endTransition = Instantiate(val.Transition, transitionTypeHolder);
                GameObject GO = Instantiate(val.BlackScreen,blackScreenCanvas.transform);
                
                endTransition.SetTransitionReferences(GO.GetComponent<Image>());

                UnityEditor.EditorUtility.SetDirty(this);

                if (_startTransitionType == _endTransitionType)
                {
                    startTransition = endTransition;
                    break;
                }
            }
        }
        
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }

    private void SetDefault()
    {
        int childCount = transitionTypeHolder.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(transitionTypeHolder.GetChild(0).gameObject);
        }
        
        childCount = blackScreenCanvas.transform.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(blackScreenCanvas.transform.GetChild(0).gameObject);
        }
    }
    
#endif
    
}
