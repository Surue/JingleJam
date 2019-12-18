using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongDrawing : MonoBehaviour {
    [SerializeField] int beatSize = 15;

    [SerializeField] int offset = 7;

    [SerializeField] int duration = 1000;

    const float beatPerSecond = 110.0f / 60.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos() {
        float nmbBeat = duration / beatPerSecond;
        
        for (int i = 0; i < nmbBeat + 1; i++) {
            Gizmos.DrawLine(new Vector3(i * beatSize + offset - 1, 10, 0), new Vector3(i * beatSize + offset - 1, -10, 0));
            Gizmos.DrawLine(new Vector3(i * beatSize + offset, 10, 0), new Vector3(i * beatSize + offset, -10, 0));
        }
    }
}
