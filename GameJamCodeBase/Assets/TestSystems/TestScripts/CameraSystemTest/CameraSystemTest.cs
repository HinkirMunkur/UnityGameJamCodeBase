using EasyButtons;
using UnityEngine;
using Munkur;

public class CameraSystemTest : MonoBehaviour
{
    [SerializeField] private EGameCameraType eGameCameraType;

    [Button]
    private void TranslateCameraTo()
    {
        CameraaManager.Instance.SetCamera(ECameraSystem.GameCameraSystem, eGameCameraType);
    }
}
