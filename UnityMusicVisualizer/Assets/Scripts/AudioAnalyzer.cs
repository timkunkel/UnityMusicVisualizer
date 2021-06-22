using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzer : MonoBehaviour
{
    [SerializeField]
    private int _sampleSize = 512;

    [SerializeField]
    private FFTWindow _fftWindow = FFTWindow.BlackmanHarris;

    private float[] _samples;
    
    private AudioSource _audioSource;

    public int     SampleSize => _sampleSize;
    public float[] Spectrum   => _samples;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _samples = new float[_sampleSize];
    }

    private void Update()
    {
        _audioSource.GetSpectrumData(_samples, 0, _fftWindow);
    }
}