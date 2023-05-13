using UnityEditor;
using UnityEngine;

public class DataEditorJSON
{
    [MenuItem("Tools/Clear ALL JSON Data")]
    private static void ClearJSON()
    {
        FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
    }
}
