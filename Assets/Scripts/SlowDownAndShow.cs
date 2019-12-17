using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownAndShow : MonoBehaviour {
    [SerializeField] bool slowDown = false;

    // Update is called once per frame
    void Update() {
        if (slowDown)
            Time.timeScale = 0.1f;
        else {
            Time.timeScale = 1.0f;
        }
    }

    void OnDrawGizmos() {
        for (int i = 0; i < 100; i++) {
            //Gizmos.DrawLine(new Vector3(i * 4, 5), new Vector3(i * 4, -5.0f));
        }
    }
}
