
[System.Serializable]
public class PlayerRecordedData : RecordedData
{
    public int playerHealt;
    
    public PlayerRecordedData() : base()
    {
        playerHealt = 100;
    }
}
