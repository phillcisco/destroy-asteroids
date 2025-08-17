using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPool : MonoBehaviour
{

    public static ProjectilesPool Instance;
    
    
    [SerializeField] List<ProjectileController> projectilesType;
    Dictionary<ProjectileType, Queue<GameObject>> _projectilePool;
    int numProjectilesType;
    int poolSize = 20;
    void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        _projectilePool = new Dictionary<ProjectileType, Queue<GameObject>>();
        foreach (var projectile in projectilesType)
        {
            CreatePool(projectile.ProjectileTypeID);
        }
    }

    void CreatePool(ProjectileType poolId)
    {
        var projectilePrefab = projectilesType.Find(p => p.ProjectileTypeID == poolId);
        var projectileQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newPrefab = Instantiate(projectilePrefab.gameObject, transform);
            newPrefab.SetActive(false);
            projectileQueue.Enqueue(newPrefab);
        }

        _projectilePool[poolId] = projectileQueue;
    }

    public void ReturnToPool(ProjectileType projectileType, GameObject projectile)
    {
        projectile.SetActive(false); 
        _projectilePool[projectileType].Enqueue(projectile);
    }

    public GameObject RetrieveFromPool(ProjectileType projectileType)
    {
        GameObject projectile = _projectilePool[projectileType].Dequeue();
        return projectile;
    }
}