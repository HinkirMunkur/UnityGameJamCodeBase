using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public enum ECameraSystem
{
    GameCameraSystem,
    MainMenuCameraSystem,
}

namespace Munkur
{
    public sealed class CameraManager : Singletonn<CameraManager>
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

        public CinemachineVirtualCamera GetActiveVirtualCamera(ECameraSystem eCameraSystem)
        {
            return GetCameraSystem(eCameraSystem).GetActiveVirtualCamera().CinemachineVirtualCamera;
        }

        public Camera GetCameraSystemMainCamera(ECameraSystem eCameraSystem)
        {
            return cameraSystemDictionary[eCameraSystem].MainCamera;
        }
        
        private ICameraTransition GetCameraSystem(ECameraSystem eCameraSystem)
        {
            return cameraSystemDictionary[eCameraSystem];
        }
    }
}
