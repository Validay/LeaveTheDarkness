using System;
using System.Collections.Generic;
using UnityEngine;

public class MemTest : MonoBehaviour
{
    public GameObject prefab;

    List<GameObject> objects;
    void Start()
    {
        objects = new List<GameObject>();
        InvokeRepeating(nameof(Clear), 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 1000; i++)
            objects.Add(Instantiate(prefab));
    }

    void Clear()
    {
        objects.ForEach(x => Destroy(x));
        objects.Clear();

        GC.Collect();
        Resources.UnloadUnusedAssets();
    }
}
