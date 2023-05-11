using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : SingletonnPersistent<DatabaseManager>
{
    [SerializeField] private bool useEncryption;

    private List<RecordedDataHandler> recordedDataHandlerList;

    private PlayerDataHandler playerDataHandler;
    public PlayerDataHandler PlayerDataHandler => playerDataHandler;

    private GameDataHandler gameDataHandler;
    public GameDataHandler GameDataHandler => gameDataHandler;

    public override void Awake()
    {
        base.Awake();
        InitRecordedDataHandlers();
    }

    private void InitRecordedDataHandlers()
    {
        recordedDataHandlerList = new List<RecordedDataHandler>();
        
        playerDataHandler = new PlayerDataHandler("PlayerData.json", new PlayerRecordedData(), useEncryption);
        recordedDataHandlerList.Add(playerDataHandler);

        gameDataHandler = new GameDataHandler("GameData.json", new GameRecordedData(), useEncryption);
        recordedDataHandlerList.Add(gameDataHandler);
    }

    public void SaveData(RecordedDataHandler recordedDataHandler)
    {
        if (!recordedDataHandler.GetRecordedData().IsDirty)
        {
            recordedDataHandler.JsonFileHandler.SaveData(recordedDataHandler.GetRecordedData());
        }
    }
    
    public void SaveGame()
    {
        foreach (var recordedDataHandler in recordedDataHandlerList)
        {
            if (!recordedDataHandler.GetRecordedData().IsDirty)
            {
                recordedDataHandler.JsonFileHandler.SaveData(recordedDataHandler.GetRecordedData());
            }
        }
    }
    
    public void LoadGame()
    {
        foreach (var recordedDataHandler in recordedDataHandlerList)
        {
            if (!recordedDataHandler.GetRecordedData().IsLoaded)
            {
                RecordedData recordedData = recordedDataHandler.JsonFileHandler.LoadData(recordedDataHandler.GetRecordedData());

                if (recordedData != null)
                {
                    recordedDataHandler.SetRecordedData(recordedData);
                }
            }
        }
    }

    public void LoadData(RecordedDataHandler recordedDataHandler)
    {
        if (!recordedDataHandler.GetRecordedData().IsLoaded)
        {
            RecordedData recordedData = recordedDataHandler.JsonFileHandler.LoadData(recordedDataHandler.GetRecordedData());

            if (recordedData != null)
            {
                recordedDataHandler.SetRecordedData(recordedData);
            }
        }
    }
    

}
