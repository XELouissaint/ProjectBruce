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
    [SerializeField] public UIHex UIHex;
    [SerializeField] public UISettlement UISettlement;
    public UICenterLeft UICenterLeft;
    public UIJobPops UIJobPops;
    public UIJobsList UIJobsList;
    public UIGameTime UIGameTime;

    public static UIController Instance;

    public static void DisplayUI(UI panel, Action setterCallback)
    {
        panel.gameObject.SetActive(true);
        panel.Initialize(setterCallback);
    }

    public static void HideUI(UI panel)
    {
        panel.gameObject.SetActive(false);
    }
    public static void CloseUI(UI panel)
    {
        panel.Close();
    }
    public static void RefreshUI(UI panel)
    {
        panel.Initialize(null);
        Debug.Log("Refresh");
    }
}
