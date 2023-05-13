
[System.Serializable]
public class PlayerRecordedData : RecordedData
{
    public string PlayerName;
    public int PlayerHealt;
    public int playerDamage;
    public bool isPlayerDead;
    
    public PlayerRecordedData() : base()
    {
        PlayerName = "DefaultName";
        PlayerHealt = 100;
        playerDamage = 5;
        isPlayerDead = false;
    }
}
