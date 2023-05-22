using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System;

namespace Munkur
{
    public interface ICameraTransition
    {
        public ECameraSystem ECameraSystem { get; }
        public void SetCamera(Enum cameraType);
    }

    public abstract class CameraSystem<ECameraType> : MonoBehaviour, ICameraTransition where ECameraType : Enum
    {
        [SerializeField] private ECameraSystem eCameraSystem;

        public ECameraSystem ECameraSystem => eCameraSystem;
        
        [SerializeField] private Transform virtualCameraHolder;
        
        [Serializable]
        public class CameraTypeVirtualCameraPair
        {
            public ECameraType CameraType;
            public VirtualCamera<ECameraType> VirtualCamera;

            public CameraTypeVirtualCameraPair(ECameraType cameraType, VirtualCamera<ECameraType> virtualCamera)
            {
                CameraType = cameraType;
                VirtualCamera = virtualCamera;
            }
        }
        
        [SerializeField] private List<CameraTypeVirtualCameraPair> cameraTypeVirtualCameraList;

        [SerializeField] private ECameraType initialCameraType;

        private Dictionary<ECameraType, VirtualCamera<ECameraType>> cameraTypeVirtualCameraDictionary;

        private ECameraType currentCameraType;

        public virtual void Awake()
        {
            cameraTypeVirtualCameraDictionary = new Dictionary<ECameraType, VirtualCamera<ECameraType>>();
            
            foreach (var cameraTypeVirtualCamera in cameraTypeVirtualCameraList)
            {
                cameraTypeVirtualCameraDictionary.Add(cameraTypeVirtualCamera.CameraType, 
                    cameraTypeVirtualCamera.VirtualCamera);
                
                cameraTypeVirtualCamera.VirtualCamera.gameObject.SetActive(false);
            }

            currentCameraType = initialCameraType;
            cameraTypeVirtualCameraDictionary[initialCameraType].gameObject.SetActive(true);
        }
        
        public void SetCamera(ECameraType cameraType)
        {
            cameraTypeVirtualCameraDictionary[currentCameraType].gameObject.SetActive(false);
            currentCameraType = cameraType;
            cameraTypeVirtualCameraDictionary[currentCameraType].gameObject.SetActive(true);
        }
        
        public void SetCamera(Enum cameraType)
        {
            cameraTypeVirtualCameraDictionary[currentCameraType].gameObject.SetActive(false);
            currentCameraType = (ECameraType)cameraType;
            cameraTypeVirtualCameraDictionary[currentCameraType].gameObject.SetActive(true);
        }
        
        private bool CheckEquality(ECameraType val1, ECameraType val2)
        {
            return !EqualityComparer<ECameraType>.Default.Equals(val1 , val2);
        }
        

    #if UNITY_EDITOR

        [Button]
        public void SetProperCameras()
        {
            if (cameraTypeVirtualCameraList != null)
            {
                cameraTypeVirtualCameraList.Clear();
            }

            cameraTypeVirtualCameraList = new List<CameraTypeVirtualCameraPair>();
            
            int childAmount = virtualCameraHolder.childCount;

            for (int i = 0; i < childAmount; i++)
            {
                VirtualCamera<ECameraType> virtualCamera = virtualCameraHolder.GetChild(i)
                    .GetComponent<VirtualCamera<ECameraType>>();
                
                if (virtualCamera != null)
                {
                    cameraTypeVirtualCameraList.Add(new CameraTypeVirtualCameraPair(virtualCamera.CameraType, virtualCamera));
                }
            }
            
            UnityEditor.EditorUtility.SetDirty(this);
            
            Debug.Log("Cameras Initialized");
        }
        
    #endif
        
    }
}
