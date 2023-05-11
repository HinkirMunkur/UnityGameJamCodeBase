using System;
using UnityEngine;

public class Player : MonoBehaviour
{
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

}
