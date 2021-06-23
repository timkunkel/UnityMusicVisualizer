using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBandCube : MonoBehaviour
{
    [SerializeField]
    private bool _useBuffer;

    [SerializeField]
    private float _bandScaleMultiplier = 10f;

    [SerializeField]
    private float _cubeSideLength = 10f;

    [SerializeField]
    private float _bufferDecreaseFactor = 1.2f;

    [SerializeField]
    private float _bufferInitialDecrease = 0.0005f;

    [SerializeField]
    private MeshRenderer _renderer;

    private int   _minFrequency;
    private int   _maxFrequency;
    private float _bandBuffer     = 0f;
    private float _bufferDecrease = 0f;

    private                 AudioAnalyzer _audioAnalyzer;
    private static readonly int           _emissionColor = Shader.PropertyToID("_EmissionColor");

    public void Initialize(int minFrequency, int maxFrequency, AudioAnalyzer audioAnalyzer)
    {
        _minFrequency  = Mathf.Min(minFrequency, maxFrequency);
        _maxFrequency  = Mathf.Max(minFrequency, maxFrequency);
        _audioAnalyzer = audioAnalyzer;
    }

    private void Update()
    {
        float bandVolume = _audioAnalyzer.BandVolume(_minFrequency, _maxFrequency, true);
        if (_useBuffer)
        {
            UpdateBuffer(bandVolume);
            bandVolume = _bandBuffer;
        }
        
        var bandColor = new Color(bandVolume, bandVolume, bandVolume);
        _renderer.material.color = bandColor;

        ScaleCubeByBandVolume(bandVolume);
    }

    private void ScaleCubeByBandVolume(float bandVolume)
    {
        float scaledHeight = bandVolume * _bandScaleMultiplier + _cubeSideLength;
        transform.localScale = new Vector3(_cubeSideLength, scaledHeight, _cubeSideLength);
    }

    private void UpdateBuffer(float bandVolume)
    {
        if (bandVolume > _bandBuffer)
        {
            _bandBuffer     = bandVolume;
            _bufferDecrease = _bufferInitialDecrease;
        }
        else
        {
            _bandBuffer     -= _bufferDecrease;
            _bufferDecrease *= _bufferDecreaseFactor;
        }
    }
}