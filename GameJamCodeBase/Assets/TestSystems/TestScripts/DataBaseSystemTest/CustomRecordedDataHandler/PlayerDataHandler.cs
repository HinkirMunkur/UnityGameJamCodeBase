
public class PlayerDataHandler : RecordedDataHandler
{
    public PlayerDataHandler(string dataFileName, PlayerRecordedData playerRecordedData, bool useEncryption) 
        : base(dataFileName, useEncryption)
    {
        this.playerRecordedData = playerRecordedData;
    }
    
    private PlayerRecordedData playerRecordedData;

    public PlayerRecordedData PlayerRecordedData
    {
        get
        {
            playerRecordedData.IsLoaded = true;
            return playerRecordedData;
        }
        set
        {
            playerRecordedData.IsDirty = true;
            playerRecordedData = value;
        }
    }
    public override RecordedData GetRecordedData()
    {
        return playerRecordedData;
    }

    public override void SetRecordedData(RecordedData recordedData)
    {
        playerRecordedData = (PlayerRecordedData)recordedData;
    }
}
