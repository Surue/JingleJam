using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSpawner : MonoBehaviour {
    [SerializeField] GameObject prefabCloud;

    [Header("Random")] 
    [SerializeField] float minHeight = 4; 
    [SerializeField] float maxHeight = 5;
    [SerializeField] float minTime = 1;
    [SerializeField] float maxTime = 5;

    float timer = 0;

    [SerializeField] float spawnPointX = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;

        if (timer < 0) {
            timer = Random.Range(minTime, maxTime);

            GameObject cloud = Instantiate(prefabCloud);
            cloud.transform.position = new Vector3(spawnPointX, Random.Range(minHeight, maxHeight), 0);
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(new Vector3(spawnPointX, maxHeight, 0), new Vector3(spawnPointX, minHeight, 0));
        Gizmos.DrawLine(new Vector3(spawnPointX - 1, maxHeight, 0), new Vector3(spawnPointX + 1, maxHeight, 0));
        Gizmos.DrawLine(new Vector3(spawnPointX - 1, minHeight, 0), new Vector3(spawnPointX + 1, minHeight, 0));
    }
}
