
public class GameDataHandler : RecordedDataHandler
{
    public GameDataHandler(string dataFileName, GameRecordedData gameRecordedData, bool useEncryption) 
        : base(dataFileName, useEncryption)
    {
        this.gameRecordedData = gameRecordedData;
    }
    
    private GameRecordedData gameRecordedData;

    public GameRecordedData GameRecordedData
    {
        get
        {
            gameRecordedData.IsLoaded = true;
            return gameRecordedData;
        }
        set
        {
            gameRecordedData.IsDirty = true;
            gameRecordedData = value;
        }
    }

    public override RecordedData GetRecordedData()
    {
        return gameRecordedData;
    }

    public override void SetRecordedData(RecordedData recordedData)
    {
        gameRecordedData = (GameRecordedData)recordedData;
    }
}
