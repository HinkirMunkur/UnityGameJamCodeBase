
[System.Serializable]
public class PlayerRecordedData : RecordedData
{
    public int PlayerHealt;

    public PlayerRecordedData() : base()
    {
        PlayerHealt = 100;
    }
}
