using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    //these are pretty good so don't change unless sure
    [SerializeField] private float fadeInRate = 0.2f;
    [SerializeField] private float fadeOutRate = 0.02f;
    [SerializeField] private float stepSize = 0.002f;

    void Awake()
    {
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySoundtrack(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            return;
        }
        float maxVol = s.source.volume;
        s.source.volume = 0;
        s.source.Play();
        StartCoroutine(FadeIn(maxVol, s));
    }
    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            return;
        }
        s.source.Play(); 
    }

    IEnumerator FadeIn(float maxVol, Sound s){
        while(s.source.volume < maxVol){
            s.source.volume += stepSize;
            yield return new WaitForSeconds(fadeInRate);
        }
        //set the volume to the volume chosen in the inspector
        s.source.volume = maxVol;
    }

    

    public void StopSoundtrack(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
           return;
        }
        float maxVol = s.source.volume;
        StartCoroutine(FadeOut(maxVol, s));
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
           return;
        }
        s.source.Stop();
    }

    IEnumerator FadeOut(float maxVol, Sound s){
        while(s.source.volume > 0){
            s.source.volume -= stepSize;
            yield return new WaitForSeconds(fadeOutRate);
        }
        s.source.volume = 0;
        s.source.Stop();
        s.source.volume = maxVol;
   
    }
}
