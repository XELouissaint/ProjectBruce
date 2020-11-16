using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void Awake()
    {
        Instance = this;
    }
    public UIPopulation UIPopulation;
    public UIPopulation DummyPopulation;
    public UISettlement UISettlement;
    public static UIController Instance;

    public void RefreshUI(UI panel)
    {
        panel.Initialize(null);
        Debug.Log("Refresh");
    }
}
