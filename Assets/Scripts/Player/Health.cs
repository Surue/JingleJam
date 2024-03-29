﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour, LevelManager.IPausedListener {
    SpriteRenderer spriteRenderer;

    HitDetector hitDetector;

    [Header("Health")] 
    [SerializeField] float healthReductionOnHit = 0.5f;

    [Header("Sounds")] 
    [SerializeField] List<AudioClip> damageSounds;

    [SerializeField] float deathZone = -12;

    [SerializeField] PlayerController playerController;
    
    AudioSource audioSource;

    bool isPaused = false;
    
    float deadTimer = 0;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        playerController = LevelManager.PlayerController;

        audioSource = GetComponent<AudioSource>();

        hitDetector = GetComponentInChildren<HitDetector>();
        
        LevelManager.Instance.AddPauseListener(this);
    }

    void Update() {
        if(isPaused) return;

        deadTimer -= Time.deltaTime;
        
        TestDamage();
    }
    
    void TestDamage() {
        if (deadTimer > 0) return;
        
        if (!playerController.HasHit && !hitDetector.HasHit && transform.position.y > deathZone) return;

        audioSource.clip = damageSounds[Random.Range(0, damageSounds.Count)];
        audioSource.Play();

        LevelManager.GameManager.PlayerDied();

        deadTimer = 2.5f;
    }

    public void OnPaused() {
        isPaused = true;
    }

    public void OnUnpaused() {
        isPaused = false;
    }
}
