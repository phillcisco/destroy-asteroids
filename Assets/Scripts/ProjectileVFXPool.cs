using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVFXPool : MonoBehaviour
{
    public static ProjectileVFXPool Instance;
    
    [SerializeField] List<GameObject> projectilesVFX;
    Queue<GameObject> projectileVFXPool;
    int numVFX;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        numVFX = projectilesVFX.Count;
        projectileVFXPool = new Queue<GameObject>(10);
        SpawnVFX();
    }

    void SpawnVFX()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject newVFX = Instantiate(projectilesVFX[0],transform);
            newVFX.SetActive(false);
            projectileVFXPool.Enqueue(newVFX);
        }
    }

    public void PlayVFX(Transform pos)
    {
        GameObject vfx = projectileVFXPool.Dequeue();
        vfx.transform.position = pos.position;
        vfx.SetActive(true);
        AddToPool(vfx);
    }
    
    public void AddToPool(GameObject vfx)
    {
        StartCoroutine(IETurnOffVFX(vfx));
    }

    IEnumerator IETurnOffVFX(GameObject vfx)
    {
        yield return new WaitForSeconds(0.3f);
        vfx.SetActive(false);
        projectileVFXPool.Enqueue(vfx);
    }
  
}
