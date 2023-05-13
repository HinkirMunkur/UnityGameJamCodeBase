using EasyButtons;
using UnityEngine;
using Munkur;

public class DataSaveLoadControllerTest : MonoBehaviour
{
    [SerializeField] private PlayerRecordedData playerRecordedData;
    [SerializeField] private GameRecordedData gameRecordedData;
    
    [Button]
    private void SaveWholeGameData()
    {
        DatabaseManager.Instance.PlayerDataHandler.PlayerRecordedData = playerRecordedData;
        DatabaseManager.Instance.GameDataHandler.GameRecordedData = gameRecordedData;
        
        DatabaseManager.Instance.SaveGame();
        Debug.Log("File Saved To: " + Application.persistentDataPath);
    }
    
    [Button]
    private void LoadWholeGameData()
    {
        DatabaseManager.Instance.LoadGame();
        
        playerRecordedData = DatabaseManager.Instance.PlayerDataHandler.PlayerRecordedData;
        gameRecordedData = DatabaseManager.Instance.GameDataHandler.GameRecordedData;
        
        Debug.Log("File Loaded From: " + Application.persistentDataPath);
    }
}
