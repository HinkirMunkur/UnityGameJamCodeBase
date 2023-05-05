using UnityEditor;
using UnityEngine;

public class PlayerPrefsDataEditor
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
