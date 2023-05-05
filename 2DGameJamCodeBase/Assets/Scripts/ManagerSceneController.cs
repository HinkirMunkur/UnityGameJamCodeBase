using System.Reflection;
using UnityEngine;


public class ManagerSceneController : SingletonnPersistent<ManagerSceneController>
{
    [SerializeField] private bool editMode;
    private const string EditModeLevelIndex = "EDIT_MODE_LEVEL_INDEX";
    
    void Start()
    {
        if (editMode)
        {
            for(int i = 1; i < LevelController.Instance.GetTotalAmountOfLevel(); i++)
                Debug.Log("index: " + i + "  " + "<color=red>" + LevelController.Instance.GetSceneNameWithIndex(i) + "</color>");
        }
        else
        {
            Destroy(this.gameObject);
            LevelController.Instance.LoadLevelWithIndex(PlayerPrefs.GetInt(EditModeLevelIndex, 1));
        }

    }
    
#if MANAGER_SCENE_EDITOR
    
    private void OnDestroy()
    {
        ClearLog();
    }

    private void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    void Update()
    {
        foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(vKey))
            {
                int keyCodeVal = (int)vKey;

                if (keyCodeVal >= 49 || keyCodeVal <= 57)
                {
                    int num = keyCodeVal - 48;
                    
                    if (num < LevelController.Instance.GetTotalAmountOfLevel())
                    {
                        LevelController.Instance.LoadLevelWithIndex(num);
                    }
                }
                /*
                else if (keyCodeVal == 275) // RIGHT ARROW
                {
                    if (LevelController.Instance.GetCurrentLevelIndex() + 1 < LevelController.Instance.GetTotalAmountOfLevel())
                    {
                        LevelController.Instance.LoadNextLevel();
                    }
                }
                else if (keyCodeVal == 276) // LEFT ARROW
                {
                    if (LevelController.Instance.GetCurrentLevelIndex() - 1 > 0)
                    {
                        LevelController.Instance.LoadLevelWithIndex(LevelController.Instance.GetCurrentLevelIndex() - 1);
                    }
                }
                */

            }
        }
 
    }
    
#endif
}
