/*
* TickLuck Team
* All rights reserved
*/

using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    public static bool is_silent;

    private void Start()
    {
        is_silent = false;
        //instance.StopAll();
        //instance.Play(INFO_HOLDER.soundTrack_buffer);
        //SoundOn();
    }

    private void Awake()
    {

        //// CODE TO SHIFT AUDIO CLIP FROM 1 SCENE TO ANOTHER
        if (instance == null) instance = this; // create obj if it is not exist

        else { Destroy(gameObject); return; } // destroy recreated objects

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.blend;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(string name)
    {
        if (!is_silent)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s.source == null) return; // code below wont be executed

            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s.source == null) return;

        s.source.Stop();
    }

    public void StopAll()
    {
        foreach (var sound in sounds)
        {
            sound.source.Stop();
        }
    }

    public void Silence()
    {
        is_silent = true;

        foreach (var sound in sounds)
        {
            sound.source.Stop();
        }
    }

    public void SoundOn()
    {
        is_silent = false;
    }
    public void PLAY_MAIN_THEME()
    {
        Play("MainTheme");
    }

}
