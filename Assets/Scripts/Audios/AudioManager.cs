using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, LevelManager.IPausedListener
{
    [SerializeField] AudioSource audioSource;

    readonly float cutOffHalf = 4000;
    readonly float cutOffMin = 10;

    float _sleepTime;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start() {
        LevelManager.Instance.AddPauseListener(this);
    }

    // Update is called once per frame
    void Update() {
        if (isPaused) return;
        
        if (!audioSource.isPlaying) {
            LevelManager.GameManager.Victory();
            Destroy(this);
        }
    }

    public void OnPaused() {
        audioSource.Pause();

        isPaused = true;
    }

    public void OnUnpaused() {
        audioSource.Play();

        isPaused = false;
    }

    public void Restart() {
        audioSource.time = 0;
        
    }
}