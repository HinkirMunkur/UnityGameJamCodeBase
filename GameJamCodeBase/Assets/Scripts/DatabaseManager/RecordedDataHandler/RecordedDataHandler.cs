using UnityEngine;

public abstract class RecordedDataHandler
{
    protected JsonFileHandler jsonFileHandler;
    public JsonFileHandler JsonFileHandler => jsonFileHandler;

    public abstract RecordedData GetRecordedData();

    public abstract void SetRecordedData(RecordedData recordedData);

    public RecordedDataHandler(string dataFileName,  bool useEncryption)
    {
        jsonFileHandler = new JsonFileHandler(Application.persistentDataPath, dataFileName, useEncryption);
    }
}

