using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourceLoader : MonoBehaviour
{
    public static ResourceLoader Instance { get; private set; }

    public void Awake() {
        Instance = this;
    }

    public Object GetResource(string resource, Type t) 
    {
        print(resource);
        //Utilizando Resources Loader
        var asset = Resources.Load(resource, t);
        if(asset) return asset;
        //Nao foi encontrado o asset
        return null;
    }

    public Object[] GetResources(string directory, Type t)
    {
        var assets = Resources.LoadAll(directory, t);
        return assets;
    }
}
