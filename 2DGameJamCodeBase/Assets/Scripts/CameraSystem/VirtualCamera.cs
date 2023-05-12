using UnityEngine;

namespace Munkur
{
    public abstract class VirtualCamera<ECameraType> : MonoBehaviour
    {
        [SerializeField] private ECameraType cameraType;
        public ECameraType CameraType => cameraType;
    }
}
