using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraBehavior : MonoBehaviour {
    Transform playerTransform;

    [Header("Camera settings")]
    [SerializeField] float maxHeight = 10;
    [SerializeField] float minHeight = -10;

    float cameraMaxHeight;
    float cameraMinHeight;

    [Header("ScreenShake Settings")]
    [SerializeField] float screenShakeAmount = 0.7f;
    [SerializeField] float screenShakeCancelDuration = 1.0f;
    float screenShakeTimeBeforStop;

    enum ShakeStat {
        NO_SHAKE,
        SHAKE_SHAKE,
        CALM_DOWN
    }

    ShakeStat shakeStat = ShakeStat.NO_SHAKE;
    
    void Start() {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        float cameraHeight = GetComponent<Camera>().orthographicSize;
        cameraMaxHeight = maxHeight - cameraHeight;
        cameraMinHeight = minHeight + cameraHeight;
    }

    void FixedUpdate() {
        transform.position = playerTransform.position;
        ShakeThat();
        CheckHeight();
    }

    void CheckHeight() {
        float y = transform.position.y;
        
        if (y > cameraMaxHeight) {
            transform.position = new Vector3(transform.position.x, cameraMaxHeight);
        } else if (y < cameraMinHeight) {
            transform.position = new Vector3(transform.position.x, cameraMinHeight);
        }

        transform.position += Vector3.back * 10;
    }


    void ShakeThat() {
        
        switch (shakeStat) {
            case ShakeStat.SHAKE_SHAKE:
                transform.position = playerTransform.position + Random.insideUnitSphere * screenShakeAmount;
                screenShakeTimeBeforStop = screenShakeCancelDuration;
                break;
            case ShakeStat.CALM_DOWN:
                transform.position = 
                    playerTransform.position +
                    Random.insideUnitSphere * screenShakeAmount * (screenShakeTimeBeforStop / screenShakeCancelDuration);
			
                screenShakeTimeBeforStop -= Time.deltaTime;

                if (screenShakeTimeBeforStop < 0)
                    shakeStat = ShakeStat.NO_SHAKE;
                break;
        }
    }
    
    public void StartScreenShake() {
        shakeStat = ShakeStat.SHAKE_SHAKE;
    }

    public void StopScreenShake() {
        shakeStat = ShakeStat.CALM_DOWN;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        float x = transform.position.x;
        Gizmos.DrawLine(new Vector3(-10 + x, maxHeight), new Vector3(10 + x, maxHeight));
        Gizmos.DrawLine(new Vector3(-10 + x, minHeight), new Vector3(10 + x, minHeight));
    }
}
