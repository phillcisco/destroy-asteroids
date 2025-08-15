using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{

    List<Transform> _powerUpSpawnPos;
    [SerializeField] List<PowerUP> _powerUps;

    int _numOfPowerup;
    int _numPowerUpPos;
    void Awake()
    {
        _powerUpSpawnPos = new List<Transform>();
        _numOfPowerup = _powerUps.Count;
        foreach(Transform spawnPos in transform)
        {
            _powerUpSpawnPos.Add(spawnPos);
        }
        _numPowerUpPos = _powerUpSpawnPos.Count;
        InvokeRepeating("SpawnPowerUP",2f,5);
    }

    void SpawnPowerUP()
    {
        int powerUp = Random.Range(0, _numOfPowerup);
        int powerUpSpawnPos = Random.Range(0, _numPowerUpPos);
        Instantiate(_powerUps[powerUp], _powerUpSpawnPos[powerUpSpawnPos].position, Quaternion.identity);
    }
}