using UnityEngine;

namespace Munkur
{
    public sealed class DataSOManager : SingletonnPersistent<DataSOManager>
    {
        [SerializeField] private GameDataSO gameDataSO;

        public GameDataSO GameDataSO => gameDataSO;
    }
}
