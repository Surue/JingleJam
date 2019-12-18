using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;

public class ApparitionEffect : MonoBehaviour {
    [SerializeField] float offsetY = 11;

    enum State {
        DOWN,
        RISING,
        GOING_DOWN,
        UP
    }

    State state = State.DOWN;

    [SerializeField] float timer = 1;
    [SerializeField] float speed = 1.5f;

    float initalTimer;
    
    [SerializeField] AnimationCurve apparitionCurve;
    Vector3 downPosition;
    
    // Start is called before the first frame update
    void Start() {
        initalTimer = timer;
        
        downPosition = transform.position + Vector3.down * offsetY;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case State.DOWN:
                
                break;
            case State.RISING:
                transform.position = downPosition + apparitionCurve.Evaluate(1 - timer * speed) * offsetY * Vector3.up ;
                timer -= Time.deltaTime;

                if (timer <= 0) {
                    state = State.UP;
                    transform.position = downPosition + Vector3.up * offsetY;

                    timer = initalTimer;
                }
                break;
            case State.UP:
                break;
            case State.GOING_DOWN:
                transform.position = downPosition + apparitionCurve.Evaluate(1 - timer * speed) * offsetY * Vector3.down ;
                timer -= Time.deltaTime;

                if (timer <= 0) {
                    state = State.DOWN;
                    transform.position = downPosition + Vector3.up * offsetY;
                    
                    timer = initalTimer;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnBecameVisible() {
        state = State.RISING;
    }

    void OnBecameInvisible() {
        state = State.GOING_DOWN;
    }
}
