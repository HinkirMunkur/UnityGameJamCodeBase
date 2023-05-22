using Munkur;
public enum EGameCameraType
{
    PreGameCamera,
    InGameCamera,
    PostGameCamera
}

public class GameCameraSystem : CameraSystem<EGameCameraType>
{
}
