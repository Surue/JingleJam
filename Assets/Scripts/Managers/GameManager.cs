﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour {
    CameraBehavior camera;
    PlayerController playerController;

    [Header("Death Stat")] [SerializeField]
    float timeBeforTP = 0.2f;
    Vector2 restartPos = Vector2.zero;

    enum DeathStat {
        NONE,
        IS_DYING,
        RESPAWN
    }

    DeathStat deathStat = DeathStat.NONE;

    void Start() {
        camera = FindObjectOfType<CameraBehavior>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate() {
        Death();
    }

    void Death() {
        switch (deathStat) {
            case DeathStat.IS_DYING:
                camera.StartScreenShake();
                break;
            case DeathStat.RESPAWN:
                camera.StopScreenShake();
                playerController.transform.position = restartPos;
                //restart music here
                break;
        }
    }

    public void PlayerDied() {
        deathStat = DeathStat.IS_DYING;
    }
}