using System.Collections.Generic;

[System.Serializable]
public class GameRecordedData : RecordedData
{
    public string CurrentLevelName; 
    public int CurrentLevel;

    public List<int> LevelGainedPoints;
    public GameRecordedData() : base()
    {
        CurrentLevelName = "DefaulLevelName";
        CurrentLevel = 3;
        
        LevelGainedPoints = new List<int>();
        LevelGainedPoints.Add(45);
        LevelGainedPoints.Add(95);
    }
}
