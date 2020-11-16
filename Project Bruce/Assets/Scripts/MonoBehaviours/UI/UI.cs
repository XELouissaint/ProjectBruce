using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;
public class UI : Client
{
    [System.NonSerialized]
    public List<GameObject> Prefabs = new List<GameObject>();

    public virtual void Initialize(Action SetterCallback)
    {
        SetterCallback?.Invoke();
        DestroyPrefabs();
    }

    public void DestroyPrefabs()
    {
        for (int i = 0; i < Prefabs.Count; i++)
        {
            Destroy(Prefabs[i]);
        }

        Prefabs = new List<GameObject>();
    }
}
