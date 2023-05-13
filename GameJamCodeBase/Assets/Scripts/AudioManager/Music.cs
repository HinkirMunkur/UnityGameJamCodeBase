using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Music", menuName = "Music")]
public class Music : ScriptableObject
{
    public AudioClip audioClip;
    public string audioName;
    
    [Range(0f, 1f)]
    public float volume;

    [Range(-3f, 3f)]
    public float pitch;

    public bool isLooping = false;

    public float delay = 0;

    /**
     * <summary>
     * Save the timestamp on the music to PlayerPrefs system so that when it's played again it doesn't restart.
     * </summary>
     */
    public void SaveCurrentTime(double time)
    {
        PlayerPrefs.SetFloat(audioName, (float)time);
    }

    /**
     * <summary>
     * Load the timestamp on the music from the PlayerPrefs system so that when it's played again it doesn't restart.
     * </summary>
     */
    public double LoadCurrentTime()
    {
        return PlayerPrefs.GetFloat(audioName, 0);
    }

    /**
     * <summary>
     * Sets the timestamp to 0.
     * </summary>
     */
    public void ResetCurrentTime()
    {
        PlayerPrefs.SetFloat(audioName, 0);
    }
}
