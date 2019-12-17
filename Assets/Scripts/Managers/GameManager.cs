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
    
    enum DeathStat {
        NONE,
        IS_DYING,
        RESPAWN
    }

    DeathStat deathStat = DeathStat.NONE;

    void Start() {
        camera = FindObjectOfType<CameraBehavior>();
        playerController = LevelManager.PlayerController;

        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        Death();
    }

    void Death() {
        switch (deathStat) {
            case DeathStat.IS_DYING:
                camera.StartScreenShake();
                timeRemaining -= Time.fixedTime;
                if (timeRemaining < 0) {
                    deathStat = DeathStat.RESPAWN;
                    timeRemaining = timeBeforTP;
                }
                break;
            case DeathStat.RESPAWN:
                camera.StopScreenShake();
                //playerController.transform.position = restartPos;
                //restart music here
                deathStat = DeathStat.NONE;
                
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    public void PlayerDied() {
        Debug.Break();
        deathStat = DeathStat.IS_DYING;

        audioSource.clip = loseSound;
        audioSource.Play();
    }
}
