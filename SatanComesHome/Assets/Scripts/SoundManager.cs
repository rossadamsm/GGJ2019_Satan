using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    public AudioClip[] dropSounds;
    public AudioClip[] priestSounds;
    public AudioClip portalsound;
    public AudioClip drop;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayLoop(AudioClip clip)
    {
        if (musicSource)
            musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public void PlayPriestSound()
    {
        PlaySingle(GetRandomClip(priestSounds));

    }

    public void PlayDropSound()
    {
        PlaySingle(GetRandomClip(dropSounds));
    }

    public void PlayPortalSound()
    {
        PlaySingle(portalsound);
    }

    public void PlayDropSoundsingle()
    {
        PlaySingle(drop);
    }

    private AudioClip GetRandomClip(AudioClip[] audioclips)
    {
        int index = Random.Range(0, audioclips.Length - 1);
        return audioclips[index];
    }
}
