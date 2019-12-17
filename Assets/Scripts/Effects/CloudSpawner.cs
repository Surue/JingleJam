using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour, LevelManager.IPausedListener {
    [SerializeField] GameObject prefabCloud;

    [Header("Random")] 
    [SerializeField] float minHeight = 4; 
    [SerializeField] float maxHeight = 5;
    [SerializeField] float minTime = 1;
    [SerializeField] float maxTime = 5;

    float timer = 0;

    [SerializeField] float offsetSpawnPointX = 10;

    bool isPaused = false;

    PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.AddPauseListener(this);

        player = LevelManager.PlayerController;
    }

    // Update is called once per frame
    void Update() {
        if (isPaused) return;
        
        timer -= Time.deltaTime;

        if (timer < 0) {
            float offsetX = player.transform.position.x;
            timer = Random.Range(minTime, maxTime);

            GameObject cloud = Instantiate(prefabCloud);
            cloud.transform.position = new Vector3(offsetSpawnPointX + offsetX, Random.Range(minHeight, maxHeight), 0);
        }
    }

    void OnDrawGizmos() {
        if (player) {
            float offsetX = player.transform.position.x;
            Gizmos.DrawLine(new Vector3(offsetSpawnPointX + offsetX, maxHeight, 0), new Vector3(offsetSpawnPointX  + offsetX, minHeight, 0));
            Gizmos.DrawLine(new Vector3(offsetSpawnPointX - 1  + offsetX, maxHeight, 0),
                new Vector3(offsetSpawnPointX + 1  + offsetX, maxHeight, 0));
            Gizmos.DrawLine(new Vector3(offsetSpawnPointX - 1  + offsetX, minHeight, 0),
                new Vector3(offsetSpawnPointX + 1  + offsetX,  minHeight, 0));
        } else {
            Gizmos.DrawLine(new Vector3(offsetSpawnPointX, maxHeight, 0), new Vector3(offsetSpawnPointX, minHeight, 0));
            Gizmos.DrawLine(new Vector3(offsetSpawnPointX - 1, maxHeight, 0),
                new Vector3(offsetSpawnPointX + 1, maxHeight, 0));
            Gizmos.DrawLine(new Vector3(offsetSpawnPointX - 1, minHeight, 0),
                new Vector3(offsetSpawnPointX + 1, minHeight, 0));
        }
    }

    public void OnPaused() {
        isPaused = true;
    }

    public void OnUnpaused() {
        isPaused = false;
    }
}
