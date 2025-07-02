using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource ambienceSource;
    public AudioSource threatSource;
    public AudioSource sfxSource;
    public AudioSource loopSource;

    [Header("Sound Effects")]
    public AudioClip footstepClip;
    public AudioClip runningBreathClip;
    public AudioClip skeletonAlertClip;
    public AudioClip switchClickClip;
    public AudioClip explosionClip;
    public AudioClip pickupClip;
    public AudioClip doorOpenClip;
    public AudioClip stickyClip;
    public AudioClip waterClip;
    public AudioClip arrowShootClip;
    public AudioClip buttonPressClip;

    [Header("Mixer Control")]
    public AudioMixer mixer;
    public string musicVolumeParam = "Volume_Music";
    public string ambienceVolumeParam = "Volume_Ambience";
    public string sfxVolumeParam = "Volume_SFX";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (ambienceSource == null || threatSource == null || sfxSource == null || loopSource == null)
        {
            var camera = GameObject.FindWithTag("MainCamera");
            if (camera != null)
            {
                var sources = camera.GetComponents<AudioSource>();
                if (sources.Length > 0) ambienceSource = sources[0];
                if (sources.Length > 1) threatSource = sources[1];
                if (sources.Length > 2) sfxSource = sources[2];
                if (sources.Length > 3) loopSource = sources[3];
            }
        }
    }

    public void PlayAmbience(AudioClip clip)
    {
        if (ambienceSource == null || clip == null) return;
        if (ambienceSource.clip != clip || !ambienceSource.isPlaying)
        {
            ambienceSource.Stop();
            ambienceSource.clip = clip;
            ambienceSource.loop = true;
            ambienceSource.Play();
        }
    }

    public void PlayThreat(AudioClip clip, float startTime = 0f)
    {
        if (threatSource == null || clip == null) return;

        bool mustRestart = threatSource.clip != clip || !threatSource.isPlaying;

        if (mustRestart)
        {
            StopAll();
            threatSource.clip = clip;
            threatSource.loop = false; 
            threatSource.time = startTime;
            threatSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void StartLoop(AudioClip clip)
    {
        if (loopSource == null || clip == null) return;

        if (loopSource.clip != clip || !loopSource.isPlaying)
        {
            loopSource.clip = clip;
            loopSource.loop = true;
            loopSource.Play();
        }
    }

    public void StopLoop()
    {
        if (loopSource != null && loopSource.isPlaying)
        {
            loopSource.Stop();
            loopSource.clip = null;
        }
    }

    public void StopSpecificLoop(AudioClip clip)
    {
        if (loopSource != null && loopSource.isPlaying && loopSource.clip == clip)
        {
            loopSource.Stop();
            loopSource.clip = null;
        }
    }

    public void PauseAll()
    {
        if (ambienceSource != null && ambienceSource.isPlaying) ambienceSource.Pause();
        if (threatSource != null && threatSource.isPlaying) threatSource.Pause();
        if (sfxSource != null && sfxSource.isPlaying) sfxSource.Pause();
        if (loopSource != null && loopSource.isPlaying) loopSource.Pause();
    }

    public void ResumeAll()
    {
        if (ambienceSource != null && ambienceSource.clip != null) ambienceSource.UnPause();
        if (threatSource != null && threatSource.clip != null) threatSource.UnPause();
        if (sfxSource != null && sfxSource.clip != null) sfxSource.UnPause();
        if (loopSource != null && loopSource.clip != null) loopSource.UnPause();
    }

    public void StopAll()
    {
        if (ambienceSource != null) ambienceSource.Stop();
        if (threatSource != null) threatSource.Stop();
        if (sfxSource != null) sfxSource.Stop();
        if (loopSource != null) loopSource.Stop();
    }

    public void MuteAll(bool mute)
    {
        if (ambienceSource != null) ambienceSource.mute = mute;
        if (threatSource != null) threatSource.mute = mute;
        if (sfxSource != null) sfxSource.mute = mute;
        if (loopSource != null) loopSource.mute = mute;
    }

    public void SetMusicVolume(float volume)
    {
        SetVolume(musicVolumeParam, volume);
    }

    public void SetAmbienceVolume(float volume)
    {
        SetVolume(ambienceVolumeParam, volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetVolume(sfxVolumeParam, volume);
    }

    private void SetVolume(string parameterName, float volume)
    {
        if (mixer == null) return;
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        mixer.SetFloat(parameterName, dB);
    }

    public void ResetInstance()
    {
        Instance = null;
    }

    public void ResetFootstep()
    {
        if (footstepClip != null)
            StartLoop(footstepClip);
    }

    public void SetFootstepOverride(AudioClip overrideClip)
    {
        if (overrideClip != null)
            StartLoop(overrideClip);
    }
}



