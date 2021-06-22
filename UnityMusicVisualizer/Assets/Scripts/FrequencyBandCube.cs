using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBandCube : MonoBehaviour
{
    private int _minFrequency;
    private int _maxFrequency;

    private AudioAnalyzer _audioAnalyzer;
    
    public void Initialize(int minFrequency, int maxFrequency, AudioAnalyzer audioAnalyzer)
    {
        _minFrequency   = Mathf.Min(minFrequency, maxFrequency);
        _maxFrequency   = Mathf.Max(minFrequency, maxFrequency);
        _audioAnalyzer = audioAnalyzer;
    }

    private void Update()
    {
        float bandVolume = _audioAnalyzer.BandVolume(_minFrequency, _maxFrequency, false);
        transform.localScale = new Vector3(10, bandVolume * bandVolume * 100 + 10, 10);
    }
}
