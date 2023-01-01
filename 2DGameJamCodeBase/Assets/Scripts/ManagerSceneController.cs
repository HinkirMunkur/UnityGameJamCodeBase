using System.Reflection;
using UnityEngine;


public class ManagerSceneController : MonoBehaviour
{
    [SerializeField] private bool editMode;
    void Start()
    {
        if (editMode)
        {
            for(int i = 1; i < LevelController.Instance.GetTotalAmountOfLevel(); i++)
                Debug.Log("index: " + i + "  " + "<color=red>" + LevelController.Instance.GetSceneNameWithIndex(i) + "</color>");
        }
        else
        {
            LevelController.Instance.LoadNextScene();
        }

    }

    private void OnDestroy()
    {
        ClearLog();
    }

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.DeleteAll();
        }
        
        foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(vKey))
            {
                switch (vKey)
                {
                    case KeyCode.Alpha1:
                        if(1 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(1);
                        break;
                    case KeyCode.Alpha2:
                        if(2 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(2);
                        break;
                    case KeyCode.Alpha3:
                        if(3 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(3);
                        break;
                    case KeyCode.Alpha4:
                        if(4 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(4);
                        break;
                    case KeyCode.Alpha5:
                        if(5 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(5);
                        break;
                    case KeyCode.Alpha6:
                        if(6 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(6);
                        break;
                    case KeyCode.Alpha7:
                        if(7 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(7);
                        break;
                    case KeyCode.Alpha8:
                        if(8 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(8);
                        break;
                    case KeyCode.Alpha9:
                        if(9 < LevelController.Instance.GetTotalAmountOfLevel())
                            LevelController.Instance.LoadLevelWithIndex(9);
                        break;
                    
                }
            }
        }
 
    }
    
}
