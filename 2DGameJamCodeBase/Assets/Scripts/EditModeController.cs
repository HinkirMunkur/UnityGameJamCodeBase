using UnityEngine.SceneManagement;
using UnityEngine;

public class EditModeController : Singletonn<EditModeController>
{
    //[SerializeField]   GAME CONFIG
    
#if EDITOR_MODE_CONTROLLER
    
    private const string EditModeLevelIndex = "EDIT_MODE_LEVEL_INDEX";
    private void Awake()
    {
        if (PlayerPrefs.GetInt(EditModeLevelIndex, 0) != SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt(EditModeLevelIndex, SceneManager.GetActiveScene().buildIndex);
            LoadManagerScene();
        }
    }

    private void LoadManagerScene()
    {
        SceneManager.LoadScene(0);
    }
    
#endif
    
}
