using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class AsteroidsSpawner : MonoBehaviour
{

    List<Transform> spawnPoints;
    Queue<GameObject> asteroidsPool;
    [SerializeField] List<GameObject> asteroidsPrefabs;
    [SerializeField] int _numSpawnPoints;
    [SerializeField] Transform asteroidsContainer;
    int _numAsteroidsVariations;

    void Awake()
    {
        spawnPoints = new List<Transform>();
        foreach(Transform spawnPoint in transform)
        {
            spawnPoints.Add(spawnPoint);
        }
        _numSpawnPoints = spawnPoints.Count;
        _numAsteroidsVariations = asteroidsPrefabs.Count;
    }

    void Start()
    {
        SpawnAsteroids(30);
        InvokeRepeating("RetrieveFromPool",0,1);
    }

    void SpawnAsteroids(int numToBeSpawned)
    {
        asteroidsPool = new Queue<GameObject>(numToBeSpawned);
        for (int i = 0; i < numToBeSpawned; i++)
        {
            int spawnPos = Random.Range(0, _numSpawnPoints);
            int chosenAsteroid = Random.Range(0, _numAsteroidsVariations);
            GameObject newAsteroid = Instantiate(asteroidsPrefabs[chosenAsteroid], spawnPoints[spawnPos].position, Quaternion.identity, asteroidsContainer);
            AsteroidController newAst = newAsteroid.GetComponent<AsteroidController>();
            newAst.OnAsteroidHit += AddToPool;
            newAsteroid.SetActive(false);
            asteroidsPool.Enqueue(newAsteroid);
        }
    }

    void AddToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        int spawnPos = Random.Range(0, _numSpawnPoints);
        gameObject.transform.position = spawnPoints[spawnPos].position;
        asteroidsPool.Enqueue(gameObject);
    }

    void RetrieveFromPool()
    {
        GameObject newAsteroid = asteroidsPool.Dequeue();
        newAsteroid.SetActive(true);
    }
    
}