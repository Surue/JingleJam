using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour {
    CameraBehavior camera;
    PlayerController playerController;

    [Header("Death Stat")]
    [SerializeField] float timeBeforTP = 0.2f;
    Vector2 restartPos = Vector2.zero;

    float timeRemaining = 0;

    [Header("Sounds")] 
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip winSound;
    AudioSource audioSource;
    
    enum GameState {
        NONE,
        IS_DYING,
        RESPAWN,
        IS_WINING,
        WIN,
    }

    GameState state = GameState.NONE;

    void Start() {
        camera = FindObjectOfType<CameraBehavior>();
        playerController = LevelManager.PlayerController;

        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        Death();
    }

    void Death() {
        switch (state) {
            case GameState.IS_DYING:
                camera.StartScreenShake();
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0) {
                    state = GameState.RESPAWN;
                    timeRemaining = timeBeforTP;
                }
                break;
            case GameState.RESPAWN:
                camera.StopScreenShake();
                //playerController.transform.position = restartPos;
                //restart music here
                state = GameState.NONE;
                
                SceneManager.LoadScene("Lose");
                break;
            case GameState.NONE:
                break;
            case GameState.IS_WINING:
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0) {
                    state = GameState.WIN;
                    timeRemaining = timeBeforTP;
                }
                break;
            case GameState.WIN:
                SceneManager.LoadScene("Win");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void PlayerDied() {
        return;
        timeRemaining = 4.0f;
        
        state = GameState.IS_DYING;

        audioSource.clip = loseSound;
        audioSource.Play();
    }

    public void Victory() {
        timeRemaining = winSound.length;
        
        state = GameState.IS_WINING;
        
        audioSource.clip = winSound;
        audioSource.Play();
    }
}
