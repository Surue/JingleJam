using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour, LevelManager.IPausedListener {
    SpriteRenderer spriteRenderer;

    GroundChecker groundChecker;
    HitDetector hitDetector;

    [Header("Snow accumulation")]
    [SerializeField] float snowAccumulationFactor = 1;
    [SerializeField] float maxSnowValue = 10;
    float snowAccumulated = 0;

    [Header("Health")] [SerializeField] float healthReductionOnHit = 0.5f;

    [Header("Sounds")] 
    [SerializeField] List<AudioClip> damageSounds;

    AudioSource audioSource;

    bool isPaused = false;

    float invulnaribitlyTimer = 1;
    float timer = 1;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        hitDetector = GetComponentInChildren<HitDetector>();
        groundChecker = GetComponentInChildren<GroundChecker>();

        audioSource = GetComponent<AudioSource>();
        
        LevelManager.Instance.AddPauseListener(this);
    }

    void Update() {
        if(isPaused) return;

        invulnaribitlyTimer -= Time.deltaTime;
        
        if (groundChecker.IsTouchingSnow) {
            spriteRenderer.color = Color.white;

            snowAccumulated += Time.deltaTime * snowAccumulationFactor;
        } else {
            spriteRenderer.color = Color.red;
        }

        TestDamage();

        transform.localScale = Vector3.one * (1 + snowAccumulated);

        if (snowAccumulated > maxSnowValue) {
            snowAccumulated = maxSnowValue;
        }
    }
    
    void TestDamage() {
        if (invulnaribitlyTimer > 0) return;
        
        if (!hitDetector.HasHit && !groundChecker.HasHit) return;

        invulnaribitlyTimer = timer;
        
        Debug.Log("1 snow = " + snowAccumulated);  
        snowAccumulated -= healthReductionOnHit;

        audioSource.clip = damageSounds[Random.Range(0, damageSounds.Count)];
        audioSource.Play();

        if (!(snowAccumulated < 0)) return;
        Debug.Log("2 snow = " + snowAccumulated);  
        snowAccumulated = 0;
        LevelManager.GameManager.PlayerDied();
        
        
    }

    public void OnPaused() {
        isPaused = true;
    }

    public void OnUnpaused() {
        isPaused = false;
    }
}
