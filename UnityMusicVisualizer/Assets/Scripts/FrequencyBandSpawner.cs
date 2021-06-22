using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBandSpawner : MonoBehaviour
{
    [SerializeField]
    private AudioAnalyzer _audioAnalyzer;

    [SerializeField]
    private FrequencyBandCube _cubePrefab;

    [SerializeField]
    private float _spaceBetweenCubes = 10f;

    private FrequencyBandCube[] _cubes;

    private void Start()
    {
        SpawnCubes();
    }

    private void SpawnCubes()
    {
        int bandCount = _audioAnalyzer.FrequencyBands.Length;
        _cubes = new FrequencyBandCube[bandCount];
        for (int i = 0; i < bandCount; i++)
        {
            FrequencyBandCube spawnedCube = Instantiate(_cubePrefab, transform, false);
            spawnedCube.transform.localPosition = new Vector3(i * _spaceBetweenCubes, 0, 0);
            FrequencyBand band = _audioAnalyzer.FrequencyBands[i];
            spawnedCube.Initialize(band.MinFrequency, band.MaxFrequency, _audioAnalyzer);
            _cubes[i] = spawnedCube;
        }
    }
}