using System;
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
    public UIJobPops UIJobPops;
    public UIJobsList UIJobsList;
    public static UIController Instance;

    public void DisplayUI(UI ui, Action setterCallback)
    {
        ui.gameObject.SetActive(true);
        ui.Initialize(setterCallback);
    }

    public void HideUI(UI ui)
    {
        ui.gameObject.SetActive(false);
    }
    public void RefreshUI(UI panel)
    {
        panel.Initialize(null);
        Debug.Log("Refresh");
    }
}
