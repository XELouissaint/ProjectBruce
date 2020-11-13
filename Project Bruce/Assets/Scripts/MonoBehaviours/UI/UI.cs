using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;
public class UI : MonoBehaviour
{
    [System.NonSerialized]
    public List<GameObject> Prefabs = new List<GameObject>();
    public static Settlement SelectedSettlement;

    public virtual void Initialize()
    {
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
