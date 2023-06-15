using UnityEngine;

public enum EVibrationLevel
{
    LIGHT,
    MEDIUM,
    HEAVY
}

public static class HapticFeedback
{
    
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate(EVibrationLevel vibrationLevel)
    {
        if(vibrationLevel == EVibrationLevel.LIGHT)
        {
            Handheld.Vibrate();
        }
        else if(vibrationLevel == EVibrationLevel.MEDIUM)
        {
            Handheld.Vibrate();
            Handheld.Vibrate();
            Handheld.Vibrate();
        }
        else if(vibrationLevel == EVibrationLevel.HEAVY)
        {
            Handheld.Vibrate();
            Handheld.Vibrate();
            Handheld.Vibrate();
            Handheld.Vibrate();
            Handheld.Vibrate();
        }
    }

    public static void Cancel()
    {
        if (IsAndroid())
        {
            vibrator.Call("cancel");
        }
    }

    public static bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
    return true;
#else
        return false;
#endif
    }
}
