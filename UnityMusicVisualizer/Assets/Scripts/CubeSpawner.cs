using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private AudioAnalyzer _audioAnalyzer;

    [SerializeField]
    private float _spaceBetweenCubes = 100f;

    [SerializeField]
    private float _maxScale = 1f;

    [SerializeField]
    private float _cubeSize = 10;

    [SerializeField]
    public GameObject _sampleCubePrefab;

    private GameObject[] _sampleCubes;

    private int _sampleSize => _audioAnalyzer.SampleSize;

    private void Start()
    {
        _sampleCubes = new GameObject[_sampleSize];

        SpawnCubes();
    }

    private void SpawnCubes()
    {
        float angularStep = 360f / _sampleSize;

        for (int i = 0; i < _sampleSize; i++)
        {
            GameObject spawnedGO = Instantiate(_sampleCubePrefab, transform);
            spawnedGO.name                  = "SampleCube" + i;
            spawnedGO.transform.eulerAngles = new Vector3(0, angularStep * i, 0);
            spawnedGO.transform.position    = spawnedGO.transform.forward * _spaceBetweenCubes;
            _sampleCubes[i]                 = spawnedGO;
        }
    }

    private void Update()
    {
        ScaleSampleCubesBasedOnSpectrum();
    }

    private void ScaleSampleCubesBasedOnSpectrum()
    {
        for (int i = 0; i < _sampleSize; i++)
        {
            _sampleCubes[i].transform.localScale =
                new Vector3(_cubeSize, (_audioAnalyzer.Spectrum[i] * _maxScale * _cubeSize), _cubeSize);
        }
    }
}