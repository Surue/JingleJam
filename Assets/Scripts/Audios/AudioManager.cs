﻿using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, LevelManager.IPausedListener
{
    [SerializeField] AudioSource audioSource;

//   readonly float _cutOffMax = 22000; old function
    readonly float cutOffHalf = 4000;
    readonly float cutOffMin = 10;

    float _sleepTime;

    // Start is called before the first frame update
    void Start() {
        LevelManager.Instance.AddPauseListener(this);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnPaused() {
        audioSource.Pause();
    }

    public void OnUnpaused() {
        audioSource.Play();
    }
}