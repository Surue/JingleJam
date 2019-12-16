
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {
    Transform playerTransform;

    [Header("Camera settings")]
    [SerializeField] float maxHeight = 10;
    [SerializeField] float minHeight = -10;

    float cameraMaxHeight;
    float cameraMinHeight;
    void Start() {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        float cameraHeight = GetComponent<Camera>().orthographicSize;
        cameraMaxHeight = maxHeight - cameraHeight;
        cameraMinHeight = minHeight + cameraHeight;
    }

    void FixedUpdate() {
        transform.position = playerTransform.position;
        CheckHeight();
    }

    void CheckHeight() {
        float y = transform.position.y;
        
        if (y > cameraMaxHeight) {
            transform.position = new Vector2(transform.position.x, cameraMaxHeight);
        } else if (y < cameraMinHeight) {
            transform.position = new Vector2(transform.position.x, cameraMinHeight);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        float x = transform.position.x;
        Gizmos.DrawLine(new Vector3(-10 + x, maxHeight), new Vector3(10 + x, maxHeight));
        Gizmos.DrawLine(new Vector3(-10 + x, minHeight), new Vector3(10 + x, minHeight));
    }
}
