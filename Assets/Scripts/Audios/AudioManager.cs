using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

//   readonly float _cutOffMax = 22000; old function
    readonly float cutOffHalf = 4000;
    readonly float cutOffMin = 10;

    float _sleepTime;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        
    }
}