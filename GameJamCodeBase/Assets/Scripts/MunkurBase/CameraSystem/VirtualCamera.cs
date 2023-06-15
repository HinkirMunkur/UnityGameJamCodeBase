using Cinemachine;
using UnityEngine;

namespace Munkur
{
    public interface IVirtualCamera
    {
        public CinemachineVirtualCamera CinemachineVirtualCamera { get; }
    }
    
    public abstract class VirtualCamera<ECameraType> : MonoBehaviour, IVirtualCamera
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        public CinemachineVirtualCamera CinemachineVirtualCamera => cinemachineVirtualCamera;
        
        [SerializeField] private ECameraType cameraType;
        public ECameraType CameraType => cameraType;
    }
}
