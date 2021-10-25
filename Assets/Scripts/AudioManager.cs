using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager one;
    public AudioClip noAudioFiller;
    public AudioClip[] audioClips;
    public Dictionary<AudioSource, string> audioSources = new Dictionary<AudioSource, string>();
    public Dictionary<string, float> volumesByGroup = new Dictionary<string, float>();

    private void Awake()
    {
        one = this;
    }

    public AudioSource PlaySound(string sName, string soundGroup = "master", bool random = false, float volumePercent = 1f)
    {
        return PlaySound(sName, soundGroup, random, volumePercent, out float x);
    }

    public AudioSource PlaySound(string sName, string soundGroup, out float clipTime)
    {
        return PlaySound(sName, soundGroup, false, 1f, out clipTime);
    }

    public AudioSource PlaySound(string sName, string soundGroup, bool random, float volumePercent, out float clipTime)
    {
        List<AudioClip> acl = new List<AudioClip>();

        foreach (AudioClip ac in audioClips)
        {
            if ((!random && ac.name == sName) || (random && ac.name.Contains(sName)))
            {
                acl.Add(ac);
            }
        }

        if (acl.Count > 0)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.volume = volumesByGroup.ContainsKey(soundGroup) ? volumesByGroup[soundGroup] * volumePercent : volumePercent;
            audioSource.clip = acl[Random.Range(0, acl.Count)];
            audioSources.Add(audioSource, soundGroup);
            audioSource.Play();
            clipTime = audioSource.clip.length;
            return audioSource;
        }
        else
        {
            if (noAudioFiller == null)
            {
                print(sName + " doesnt exist.");
                clipTime = 0f;
                return null;
            }
            else
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.loop = false;
                audioSource.volume = volumesByGroup.ContainsKey(soundGroup) ? volumesByGroup[soundGroup] * volumePercent : volumePercent;
                audioSource.clip = noAudioFiller;
                audioSources.Add(audioSource, soundGroup);
                audioSource.Play();
                clipTime = audioSource.clip.length;
                return audioSource;
            }
        }
    }

    public void SetVolume(string soundGroup, float volume)
    {
        if (volumesByGroup.ContainsKey(soundGroup))
        {
            volumesByGroup[soundGroup] = volume;
        }
        else
        {
            volumesByGroup.Add(soundGroup, volume);
        }
        foreach (KeyValuePair<AudioSource, string> sources in audioSources)
        {
            if (sources.Value == soundGroup)
            {
                sources.Key.volume = volume;
            }
        }
    }

    public void MuteAll(bool mute)
    {
        foreach (AudioSource source in audioSources.Keys)
        {
            source.mute = mute;
        }
    }

    public void PauseSounds(bool pause, string soundGroup)
    {
        if (pause)
            foreach (KeyValuePair<AudioSource, string> sources in audioSources)
            {
                if (sources.Value == soundGroup)
                {
                    sources.Key.Pause();
                }
            }
        else
            foreach (KeyValuePair<AudioSource, string> sources in audioSources)
            {
                if (sources.Value == soundGroup)
                {
                    sources.Key.UnPause();
                }
            }
    }
    public void PauseAllSounds(bool pause)
    {
        if (pause)
            foreach (AudioSource source in audioSources.Keys)
            {
                source.Pause();
            }
        else
            foreach (AudioSource source in audioSources.Keys)
            {
                source.UnPause();
            }

    }

    public void StopSounds(string soundGroup)
    {
        foreach (KeyValuePair<AudioSource, string> sources in audioSources)
        {
            if (sources.Value == soundGroup)
            {
                sources.Key.Stop();
            }
        }
    }
    public void StopAllSounds()
    {
        foreach (AudioSource source in audioSources.Keys)
        {
            source.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<AudioSource> aSR = new List<AudioSource>();
        foreach (AudioSource aS in audioSources.Keys)
        {
            if (!aS.isPlaying)
            {
                aSR.Add(aS);
            }
        }
        foreach (AudioSource aS in aSR)
        {
            audioSources.Remove(aS);
            Destroy(aS);
        }
    }
}
