using System;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{

    [SerializeField] List<Transform> spawnPoints;

    float _numSpawnPoints;

    void Awake()
    {
        spawnPoints = new List<Transform>();
        foreach(Transform spawnPoint in transform)
        {
            spawnPoints.Add(spawnPoint);
        }
        _numSpawnPoints = spawnPoints.Count;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
