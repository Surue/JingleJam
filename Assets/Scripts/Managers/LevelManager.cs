using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public interface IPausedListener {
        void OnPaused();
        void OnUnpaused();
    }
    
    List<IPausedListener> callbacks = new List<IPausedListener>();

    static LevelManager instance = null;

    int val = 10;

    [SerializeField] AudioPeer audioPeer;
    
    public static LevelManager Instance => instance;
    
    public static int Value => instance.val;

    public static AudioPeer AudioPeer => instance.audioPeer;


    static GameManager _gameManager;

    public static GameManager gameManager => _gameManager;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPauseListener(IPausedListener listener) {
        callbacks.Add(listener);
    }

    public void Pause() {
        foreach (IPausedListener pausedListener in callbacks) {
            pausedListener.OnPaused();
        }
    }

    public void Unpause() {
        foreach (IPausedListener pausedListener in callbacks) {
            pausedListener.OnUnpaused();
        }
    }
}
