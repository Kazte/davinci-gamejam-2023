using System;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;

public enum TypeSound
{
    SoundGroup,
    MusicGroup
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioMixerGroup _musicMixerGroup;
    [SerializeField] AudioMixerGroup _soundMixerGroup;
    [SerializeField] Sound[] _sounds = new Sound[0];


    string currentMusic = "";

    protected void Awake()
    {
        foreach (var s in _sounds)
        {
            // asignando los componentes del AudioMixer
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            // asignando los componentes basicos
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.loop;

            switch (s.type)
            {
                case TypeSound.SoundGroup:
                    s.source.outputAudioMixerGroup = _soundMixerGroup;
                    break;
                case TypeSound.MusicGroup:
                    s.source.outputAudioMixerGroup = _musicMixerGroup;
                    break;
            }

            if (s.playOnAwake)
            {
                s.source.Play();
                currentMusic = s.name;
            }
        }
    }

    /// <summary>
    /// Reproduce la canción que le pases
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isMusic"></param>
    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, sonido => sonido.name == name);

        if (s != null)
        {
            // En el caso de que la cancion sea musica
            if (s.type == TypeSound.MusicGroup)
            {
                if (IsAudioPlaying(name)) return;

                Stop(currentMusic);
                currentMusic = name;
            }

            s.source.PlayOneShot(s.clip);
        }
    }

    /// <summary>
    /// Detiene la canción que le pases
    /// </summary>
    /// <param name="name"></param>
    public void Stop(string name)
    {
        Sound s = Array.Find(_sounds, sonido => sonido.name == name);
        if (s != null)
            s.source.Stop();
    }

    /// <summary>
    /// Verifica que camción se está reproduciendo actualmente
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsAudioPlaying(string name)
    {
        bool result = false;

        Sound s = Array.Find(_sounds, sonido => sonido.name == name);
        if (s != null) //si hay un sonido que coincide
        {
            if (s.source.isPlaying)
                result = true;
        }

        return result;
    }
}