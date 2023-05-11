using UnityEngine;

public class RecordedDataHandler
{
    protected JsonFileHandler jsonFileHandler;
    public JsonFileHandler JsonFileHandler => jsonFileHandler;

    protected RecordedData recordedData;

    public RecordedData RecordedData
    {
        get
        {
            //recordedData.IsLoaded = true;
            return recordedData;
        }
        set
        {
            //recordedData.IsDirty = true;
            recordedData = value;
        }
    }

    public RecordedDataHandler(string dataFileName, RecordedData recordedData,  bool useEncryption)
    {
        this.recordedData = recordedData;
        jsonFileHandler = new JsonFileHandler(Application.persistentDataPath, dataFileName, useEncryption);
    }
}

