using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioFile[] audioFiles;

    private float timeToReset;
    private bool timerIsSet = false;

    private string tmpName;
    private float tmpVol;

    private bool isLowered = false;

    private bool fadeOut = false;
    private bool fadeIn = false;
    private string fadeInUsedString;
    private string fadeOutUsedString;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        foreach (AudioFile clip in audioFiles)
        {
            clip.source = gameObject.AddComponent<AudioSource>();
            clip.source.clip = clip.audioClip;
            clip.source.volume = clip.volume;
            clip.source.loop = clip.isLooping;
            clip.source.spatialBlend = clip._3dSound;
            clip.source.pitch = clip.pitch;
            if (clip.playOnAwake)            
                clip.source.Play();            
        }
    }

    private void FixedUpdate()
    {
        //transform.position = GameController.Instance.CubeControl.transform.position;
    }
    public static void PlayMusic(string name)
    {
        AudioFile clip = Array.Find(Instance.audioFiles, AudioFile => AudioFile.audioName == name);
        if (clip == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            return;
        }
        else
        {
            clip.source.Play();
        }
    }

    public static void StopMusic(String name)
    {
        AudioFile clip = Array.Find(Instance.audioFiles, AudioFile => AudioFile.audioName == name);
        if (clip == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            return;
        }
        else
        {
            clip.source.Stop();
        }
    }

    public static void PauseMusic(String name, SongState state)
    {
        AudioFile clip = Array.Find(Instance.audioFiles, AudioFile => AudioFile.audioName == name);
        if (clip == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            return;
        }
        else
        {
            clip.source.Pause();
        }
    }

    public static void UnPauseMusic(String name)
    {
        AudioFile clip = Array.Find(Instance.audioFiles, AudioFile => AudioFile.audioName == name);
        if (clip == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            return;
        }
        else
        {
            clip.source.UnPause();
        }
    }

    public static void LowerVolume(String name, float _duration)
    {
        if (Instance.isLowered == false)
        {
            AudioFile clip = Array.Find(Instance.audioFiles, AudioFile => AudioFile.audioName == name);
            if (clip == null)
            {
                Debug.LogError("Sound name" + name + "not found!");
                return;
            }
            else
            {
                Instance.tmpName = name;
                Instance.tmpVol = clip.volume;
                Instance.timeToReset = Time.time + _duration;
                Instance.timerIsSet = true;
                clip.source.volume = clip.source.volume / 3;
            }
            Instance.isLowered = true;
        }
    }

    private void Update()
    {
        if (Time.time >= timeToReset && timerIsSet)
        {
            ResetVol();
            timerIsSet = false;
        }
    }

    void ResetVol()
    {
        AudioFile clip = Array.Find(Instance.audioFiles, AudioFile => AudioFile.audioName == tmpName);
        clip.source.volume = tmpVol;
        isLowered = false;
    }

    public static void FadeIn(String name, float targetVolume, float duration)
    {
        Instance.StartCoroutine(Instance.IFadeIn(name, targetVolume, duration));
    }

    public static void FadeOut(String name, float duration)
    {
        Instance.StartCoroutine(Instance.IFadeOut(name, duration));
    }

    public IEnumerator IFadeIn(string name, float targetVolume, float duration)
    {
        AudioFile clip = Array.Find(Instance.audioFiles, AudioFile => AudioFile.audioName == name);
        if (clip == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            yield return null;
        }
        else
        {
            if (fadeIn == false)
            {
                fadeIn = true;
                Instance.fadeInUsedString = name;
                clip.source.volume = 0f;
                clip.source.Play();
                while (clip.source.volume < targetVolume)
                {
                    clip.source.volume += Time.deltaTime / duration;
                    yield return null;
                }

                yield return new WaitForSeconds(duration);
                fadeIn = false;
            }
            else
            {
                Debug.Log("Could not handle two fade ins at once: " + name + " , " + fadeInUsedString + "! Played the music " + name);
                StopMusic(fadeInUsedString);
                PlayMusic(name);
            }
        }
    }

    private IEnumerator IFadeOut(String name, float duration)
    {
        AudioFile clip = Array.Find(Instance.audioFiles, AudioFile => AudioFile.audioName == name);
        if (clip == null)
        {
            Debug.LogError("Sound name" + name + "not found!");
            yield return null;
        }
        else
        {
            if (fadeOut == false)
            {
                fadeOut = true;
                float startVol = clip.source.volume;
                fadeOutUsedString = name;
                while (clip.source.volume > 0)
                {
                    clip.source.volume -= startVol * Time.deltaTime / duration;
                    yield return null;
                }

                clip.source.Stop();
                yield return new WaitForSeconds(duration);
                fadeOut = false;
            }

            else
            {
                Debug.Log("Could not handle two fade outs at once : " + name + " , " + fadeOutUsedString + "! Stopped the music " + name);
                StopMusic(name);
            }
        }
    }


}
