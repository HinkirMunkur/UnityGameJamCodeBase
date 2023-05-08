using UnityEngine;

public class DataSOManager : SingletonnPersistent<DataSOManager>
{
    [SerializeField] private GameDataSO gameDataSO;

    public GameDataSO GameDataSO => gameDataSO;
}
