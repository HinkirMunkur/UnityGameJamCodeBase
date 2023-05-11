
[System.Serializable]
public class GameRecordedData : RecordedData
{
    public int currentLevel;
    
    public GameRecordedData() : base()
    {
        currentLevel = 3;
    }
}
