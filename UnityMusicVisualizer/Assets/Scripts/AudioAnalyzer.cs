using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzer : MonoBehaviour
{
    private int SourceSampleRate => AudioSettings.outputSampleRate;

    [SerializeField]
    private int _sampleSize = 512;

    [SerializeField]
    private FFTWindow _fftWindow = FFTWindow.BlackmanHarris;

    private float[] _samples;

    [SerializeField]
    private FrequencyBand[] _frequencyBands;

    private AudioSource _audioSource;

    public int             SampleSize     => _sampleSize;
    public float[]         Spectrum       => _samples;
    public FrequencyBand[] FrequencyBands => _frequencyBands;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _samples     = new float[_sampleSize];
    }

    private void Update()
    {
        UpdateSpectrumData();
    }

    private void UpdateSpectrumData()
    {
        _audioSource.GetSpectrumData(_samples, 0, _fftWindow);
    }

    public float BandVolume(float minFrequency, float maxFrequency, bool average)
    {
        minFrequency = Mathf.Clamp(minFrequency, 20,           maxFrequency); // limit low...
        maxFrequency = Mathf.Clamp(maxFrequency, minFrequency, SourceSampleRate); // and high frequencies

        int   n1  = (int) Mathf.Floor(minFrequency * _sampleSize / SourceSampleRate);
        int   n2  = (int) Mathf.Floor(maxFrequency * _sampleSize / SourceSampleRate);
        float sum = 0;

        // average of all frequencies
        for (int i = n1; i <= n2; i++)
        {
            sum += _samples[i];
        }

        return average ? sum / (n2 - n1 + 1) : sum;
    }
}

[Serializable]
public class FrequencyBand
{
    public int MinFrequency;
    public int MaxFrequency;
}