using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemTest : MonoBehaviour
{
    public GameObject prefab;
    int countr = 0;

    GameObject[] objects1 = new GameObject[100000];
    void Start()
    {
        objects1 = new GameObject[100000];
        InvokeRepeating(nameof(Clear), 20f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (countr < objects1.Length)
            objects1[countr] = new GameObject();

        countr++;
    }

    void Clear()
    {
        for (int i = 0; i < objects1.Length; i++)
        {
            Destroy(objects1[i]);
            objects1[i] = null;
        }

        GC.Collect();

        countr = 0;

        SceneManager.LoadScene("TestScene", LoadSceneMode.Additive);
    }
}
