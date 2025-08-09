using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AsteroidsSpawner : MonoBehaviour
{

    List<Transform> spawnPoints;

    [SerializeField] List<GameObject> asteroids;

    int _numSpawnPoints;
    int _numAsteroidsVariations;

    float _elapsedTime;
    
    void Awake()
    {
        spawnPoints = new List<Transform>();
        foreach(Transform spawnPoint in transform)
        {
            spawnPoints.Add(spawnPoint);
        }
        _numSpawnPoints = spawnPoints.Count;
    }

    void Start()
    {
        _numAsteroidsVariations = asteroids.Count;
        InvokeRepeating("SpawnAsteroid",0,1);
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
    }

    void SpawnAsteroid()
    {
        int spawnPos = Random.Range(0, _numSpawnPoints);
        int chosenAsteroid = Random.Range(0, _numAsteroidsVariations);
        Instantiate(asteroids[chosenAsteroid], spawnPoints[spawnPos].position, Quaternion.identity);
    }
}
