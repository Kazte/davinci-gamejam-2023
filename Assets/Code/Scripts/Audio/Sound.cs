using UnityEngine;

[System.Serializable]
public class Sound
{
    [Header("Basic")]
    public string name;

    [Range(0, 1)] public float volume;
    [Range(-3, 3)] public float pitch;
    public bool playOnAwake;
    public bool loop;

    [Header("Component")]
    public AudioClip clip;

    [HideInInspector] public AudioSource source;
    public TypeSound type;
}