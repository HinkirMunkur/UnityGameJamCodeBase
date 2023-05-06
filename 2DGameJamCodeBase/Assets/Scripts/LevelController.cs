using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LevelController : SingletonnPersistent<LevelController>
{
    public void LoadLevelWithIndex(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadCurrLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public int GetTotalAmountOfLevel()
    {
        return SceneManager.sceneCountInBuildSettings;
    }

    public string GetSceneNameWithIndex(int index)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(index);
        return path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
    }

    public Scene GetCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void SetCurrentSceneDirty()
    {
        EditorSceneManager.MarkSceneDirty(GetCurrentScene());
    }

    public string GetScenePathWithIndex(int index)
    {
        return SceneUtility.GetScenePathByBuildIndex(index);
    }

    public int GetCurrentLevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

}
