using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System;

public enum ECameraSystem
{
    GameCamSystem,
    Player2CamSystem,
}

public class CameraaManager : Singletonn<CameraaManager>
{
    [SerializeField] private Transform cameraSystemsHolder;
    
    [Serializable]
    public class CameraSystemTypePair
    {
        public ECameraSystem ECameraSystem;
        public CameraSystem<Enum> CameraSystem;

        public CameraSystemTypePair(ECameraSystem eCameraSystem, CameraSystem<Enum> cameraSystem)
        {
            ECameraSystem = eCameraSystem;
            CameraSystem = cameraSystem;
        }
    }

    [SerializeField] private List<CameraSystemTypePair> cameraSystemTypePairList;
    
    private Dictionary<ECameraSystem, CameraSystem<Enum>> cameraSystemDictionary;

    private void Awake()
    {
        foreach (var cameraSystemTypePair in cameraSystemTypePairList)
        {
            cameraSystemDictionary.Add(cameraSystemTypePair.ECameraSystem, cameraSystemTypePair.CameraSystem);
        }
    }

    public void SetCamera(ECameraSystem eCameraSystem, Enum eCameraType)
    {
        CameraSystem<Enum> cameraSystem = GetCameraSystem(eCameraSystem);
        cameraSystem.SetCamera(eCameraType);
    }
    
    private CameraSystem<Enum> GetCameraSystem(ECameraSystem eCameraSystem)
    {
        return cameraSystemDictionary[eCameraSystem];
    }
    
#if UNITY_EDITOR

    [Button]
    public void SetProperCameras()
    {
        if (cameraSystemTypePairList != null)
        {
            cameraSystemTypePairList.Clear();
        }

        cameraSystemTypePairList = new List<CameraSystemTypePair>();
        
        int childAmount = cameraSystemsHolder.childCount;

        for (int i = 0; i < childAmount; i++)
        {
            //CameraSystem<Enum> cameraSystem = cameraSystemsHolder.GetChild(i).GetComponent<>();
            
            //cameraSystemTypePairList.Add(new CameraSystemTypePair(cameraSystem.ECameraSystem, cameraSystem));
            //cameraSystem.SetProperCameras();
        }
        
        
        Debug.Log("CameraSystems Initialized");
    }
    
#endif
}
