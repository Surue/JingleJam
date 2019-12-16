using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTest : MonoBehaviour, AudioPeer.ISpikeAudioListener {
    List<GameObject> objects;
    List<GameObject> upperBar;
    AudioPeer processor;

    List<float> timers;
    
    // Start is called before the first frame update
    void Awake()
    {
        processor = FindObjectOfType<AudioPeer>();
        processor.AddCallbacks(this);
        
        objects = new List<GameObject>();
        upperBar = new List<GameObject>();
        timers = new List<float>();
        
        for (int i = 0; i < processor.freqBand.Length; i++) {
            //Columns
            {
                GameObject o = new GameObject();

                o.transform.position = new Vector3(i - processor.freqBand.Length * 0.5f, 0, 0);
                o.transform.localScale = new Vector3(1, 0, 1);
                o.transform.parent = transform;

                o.AddComponent<SpriteRenderer>();
                o.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;

                objects.Add(o);
            }
            //Upper bar
            {
                GameObject o = new GameObject();

                o.transform.position = new Vector3(i - processor.freqBand.Length * 0.5f, 0.1f, 0);
                o.transform.localScale = new Vector3(1, 0.2f, 1);
                o.transform.parent = transform;

                o.AddComponent<SpriteRenderer>();
                o.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                o.GetComponent<SpriteRenderer>().color = Color.red;

                upperBar.Add(o);
            }
            
            timers.Add(0);
        }
        
        Destroy(GetComponent<SpriteRenderer>());
    }

    float scaleFactor = 5;
    
    void Update() {
        for (int i = 0; i < processor.freqBand.Length; i++) {    
            objects[i].transform.localScale = new Vector3(1, processor.freqBand[i] * scaleFactor, 1);
            
            if (upperBar[i].transform.position.y < processor.freqBand[i] * scaleFactor + 0.1f) {
                timers[i] = 1.1f;
                upperBar[i].transform.position = new Vector3(upperBar[i].transform.position.x,(processor.freqBand[i] * scaleFactor) + 0.1f, upperBar[i].transform.position.z);
            }
        }
        
        for (int i = 0; i < processor.freqBand.Length; i++) {
            if (timers[i] > 0) {
                upperBar[i].transform.position =
                    Vector3.Lerp(new Vector3(upperBar[i].transform.position.x, 0.1f, upperBar[i].transform.position.y),
                        upperBar[i].transform.position, timers[i]);
                timers[i] -= Time.deltaTime;
            }
        }
    }

    public void OnAudioSpike(float[] freqBand) {
//        Debug.Log("spike");
    }
}
