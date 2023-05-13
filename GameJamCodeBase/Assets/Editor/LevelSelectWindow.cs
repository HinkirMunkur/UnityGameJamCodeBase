using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class LevelSelectWindow : EditorWindow
{
    [MenuItem("Tools/LevelSelectWindow %#l")]
    public static void ShowWindow()
    {
        GetWindow<LevelSelectWindow>("Select Level");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("SELECT LEVEL (EDITOR MODE)\n");

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            EditorGUILayout.BeginHorizontal();
            
            GUILayout.Label($"({i})  {GetSceneNameWithIndex(i)}", EditorStyles.boldLabel);

            if (GUILayout.Button("Open", GUILayout.Height(20), GUILayout.Width(60)))
            {
                EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
                EditorSceneManager.OpenScene(GetScenePathWithIndex(i));
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
    
    private string GetSceneNameWithIndex(int index)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(index);
        return path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
    }
    
    private string GetScenePathWithIndex(int index)
    {
        return SceneUtility.GetScenePathByBuildIndex(index);
    }
}
