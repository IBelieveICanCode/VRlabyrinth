using UnityEditor;
using UnityEngine;

public enum SongState {Play, Stop, Pause, UnPause}
[System.Serializable]
public class AudioFile
{
    public string audioName;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float _3dSound;

    [Range(1f, 3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;

    public bool isLooping;

    public bool playOnAwake;

}