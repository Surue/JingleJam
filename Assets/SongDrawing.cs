using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongDrawing : MonoBehaviour {
    [SerializeField] int beatSize = 15;

    [SerializeField] int offset = 7;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos() {
        for (int i = 0; i < 1000; i++) {
            Gizmos.DrawLine(new Vector3(i * beatSize + offset - 1, 10, 0), new Vector3(i * beatSize + offset - 1, -10, 0));
            Gizmos.DrawLine(new Vector3(i * beatSize + offset, 10, 0), new Vector3(i * beatSize + offset, -10, 0));
        }
    }
}
