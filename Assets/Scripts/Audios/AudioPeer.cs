using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPeer : MonoBehaviour
{
    public interface ISpikeAudioListener
    {
        void OnAudioSpike(float[] freqBand);
    }

    const int NUM_BAND = 24;

    float[] Samples = new float[2048];
    public readonly float[] freqBand = new float[NUM_BAND];

    readonly List<ISpikeAudioListener> callbacks = new List<ISpikeAudioListener>();
    float[] window;
    int windowFill = 0;
    const int WINDOW_SIZE = 20;

    AudioListener audioListener;
    
    [SerializeField] AudioSource audioSource;

    [SerializeField] int bpm = 110;

    // Start is called before the first frame update
    void Start()
    {
        window = new float[WINDOW_SIZE];
//        StartCoroutine(BpmSpike());
    }

    IEnumerator BpmSpike() {
        float timer = 60.0f / bpm;

        while (audioSource.isPlaying) {
            RegisterCallback(freqBand);
            yield return new WaitForSeconds(timer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(Samples, 0, FFTWindow.BlackmanHarris);
        MakeFrequencyBand();
    }

    public void AddCallbacks(ISpikeAudioListener listener)
    {
        callbacks.Add(listener);
    }

    void RegisterCallback(float[] freqBand)
    {
        foreach (ISpikeAudioListener t in callbacks) {
            t.OnAudioSpike(freqBand);
        }
    }

    void MakeFrequencyBand()
    {
        int count = 0;
        for (int i = 0; i < NUM_BAND; i++) {
            float average = 0;
            int sampleCount = (int) Mathf.Pow(2, i - 14) + 1;

            for(int j = 0; j < sampleCount; j++) {
                average += Samples[count] * (count + 1);
                count++;
            }

            average /= sampleCount;

            freqBand[i] = average;
        }

        float sum = 0;
        Array.ForEach(freqBand, delegate(float i) { sum += i;});
        float avgFreq = sum / freqBand.Length;

        if (windowFill + 1 >= WINDOW_SIZE) {
            sum = 0;
            Array.ForEach(window, delegate(float i) { sum += i;});
            float avgWindow = sum / window.Length;

            if (avgFreq > avgWindow) {
                RegisterCallback(freqBand);
            }

            window[(windowFill++) % WINDOW_SIZE] = avgFreq;
        } else {
            window[windowFill] = avgFreq;
            windowFill++;
        }
    }
}
