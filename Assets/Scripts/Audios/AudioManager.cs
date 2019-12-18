using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, LevelManager.IPausedListener
{
    [SerializeField] AudioSource audioSource;

    readonly float cutOffHalf = 4000;
    readonly float cutOffMin = 10;

    float _sleepTime;

    bool isPaused = false;
    
    float timer = 0;

    // Start is called before the first frame update
    void Start() {
        LevelManager.Instance.AddPauseListener(this);

        timer = audioSource.clip.length;
    }

    // Update is called once per frame
    void Update() {
        if (isPaused) return;

        timer -= Time.deltaTime;

        if (timer <= 0) {
            LevelManager.GameManager.Victory();
            timer = 10000;
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
        isPaused = false;
        audioSource.time = 0;
        timer = audioSource.clip.length;
        audioSource.Play();
    }

    public void Stop() {
        isPaused = true;
        audioSource.Stop();
    }
}