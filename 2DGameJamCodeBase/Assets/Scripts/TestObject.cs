using System;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CameraaManager.Instance.SetCamera(ECameraSystem.GameCamSystem, EGameCameraType.Idle);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            CameraaManager.Instance.SetCamera(ECameraSystem.GameCamSystem, EGameCameraType.Walk);
        }
    }

}
