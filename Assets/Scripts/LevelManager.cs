using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    static LevelManager instance = null;

    int val = 10;

    [SerializeField] AudioPeer audioPeer;
    
    public static LevelManager Instance => Instance;
    
    
    public static int Value => instance.val;

    public static AudioPeer AudioPeer => instance.audioPeer;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
