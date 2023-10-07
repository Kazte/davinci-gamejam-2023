using UnityEngine;


[System.Serializable]
public class AudioPlayer
{
    private readonly AudioClip[] clips;
    private AudioSource source;
    private Vector2 pitch;
    private float initPitch;

    public static AudioPlayer Create(AudioClip[] clips)
    {
        return new AudioPlayer(clips);
    }

    public static AudioPlayer Create(AudioClip clip)
    {
        return new AudioPlayer(new AudioClip[] { clip });
    }

    private AudioPlayer(AudioClip[] clips)
    {
        this.clips = clips;
        initPitch = 1;
    }

    public AudioPlayer WithAnchor(Transform transform)
    {
        source = transform.gameObject.AddComponent<AudioSource>();
        return this;
    }


    public AudioPlayer WithPitch(float min, float max, float inital = 1)
    {
        pitch.Set(min, max);
        initPitch = inital;
        return this;
    }

    public AudioPlayer PlayOnAwake(bool set)
    {
        source.playOnAwake = set;
        return this;
    }

    public void PlayLoop()
    {
        source.loop = true;
        source.clip = clips[Random.Range(0, clips.Length - 1)];
        source.pitch = initPitch + Random.Range(pitch.x, pitch.y);
        source.Play();
    }

    public void PlayOnce()
    {
        source.loop = false;
        source.pitch = initPitch + Random.Range(pitch.x, pitch.y);
        source.PlayOneShot(clips[Random.Range(0, clips.Length - 1)]);
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Pause()
    {
        source.Pause();
    }
}