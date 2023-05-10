using System.Collections.Generic;
using UnityEngine;
using System;

public enum ECameraSystem
{
    GameCamSystem,
    Player2CamSystem,
}

public class CameraaManager : Singletonn<CameraaManager>
{
    [SerializeField] private Transform cameraSystemHolder;
        
    private Dictionary<ECameraSystem, ICameraTransition> cameraSystemDictionary;

    private void Awake()
    {
        cameraSystemDictionary = new Dictionary<ECameraSystem, ICameraTransition>();

        ICameraTransition[] cameraSystems = cameraSystemHolder.GetComponentsInChildren<ICameraTransition>();

        foreach (var cameraSystem in cameraSystems)
        {
            cameraSystemDictionary.Add(cameraSystem.ECameraSystem, cameraSystem);
        }
    }

    public void SetCamera(ECameraSystem eCameraSystem, Enum eCameraType)
    {
        ICameraTransition cameraTransitionSystem = GetCameraSystem(eCameraSystem);
        cameraTransitionSystem.SetCamera(eCameraType);
    }
    
    private ICameraTransition GetCameraSystem(ECameraSystem eCameraSystem)
    {
        return cameraSystemDictionary[eCameraSystem];
    }
}
