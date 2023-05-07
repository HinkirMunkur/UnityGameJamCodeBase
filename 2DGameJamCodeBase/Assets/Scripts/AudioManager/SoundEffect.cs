using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Effect", menuName = "Sound Effect")]
public class SoundEffect : ScriptableObject
{
    public AudioClip audioClip;
    public string audioName;

    [Range(0f, 1f)]
    public float volume;
}
