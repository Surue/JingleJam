using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour, LevelManager.IPausedListener {
    [SerializeField] float speed = 5;
    [SerializeField] float lifeTime = 5;

    bool isPaused = false;

    void Start() {
        LevelManager.Instance.AddPauseListener(this);
    }

    // Update is called once per frame
    void Update() {
        if (isPaused) return;
        
        transform.position += speed * Time.deltaTime * Vector3.left;

        lifeTime -= Time.deltaTime;
        
        if(lifeTime < 0)
            Destroy(gameObject);
    }

    public void OnPaused() {
        isPaused = true;
    }

    public void OnUnpaused() {
        isPaused = false;
    }
}
