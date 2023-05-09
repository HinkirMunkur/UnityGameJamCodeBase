using System;
using UnityEngine;

public class TestObject : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int a, b, c;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            DatabaseManager.Instance.LoadGame();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            DatabaseManager.Instance.SaveGame();
        }
    }

    public void LoadData(GameData data)
    {
        a = data.a;
        b = data.b;
        c = data.c;
    }

    public void SaveData(ref GameData data)
    {
        data.a = a;
        data.b = b;
        data.c = c;
    }
}
