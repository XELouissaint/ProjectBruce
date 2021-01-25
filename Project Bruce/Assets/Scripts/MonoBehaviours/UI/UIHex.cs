using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class UIHex : UI
{
    public override void Initialize(Action SetterCallback)
    {
        base.Initialize(SetterCallback);
        if(SelectedHex == null)
        {
            return;
        }

        UIController.DisplayUI(UICenterLeft, null);

        foreach (Resource resource in SelectedHex.Economy.ResourcesAvailable.Keys)
        {
            ResourceAvailableItem resourceItem = Instantiate(ResourceItemPrefab, ResourceHolder);

            resourceItem.NameText.text = resource.name;
            resourceItem.CountText.text = SelectedHex.Economy.ResourcesAvailable[resource].ToString();
            Prefabs.Add(resourceItem.gameObject);

        }
    }
    public override void Close()
    {
        UIController.HideUI(UICenterLeft);
    }

    public ResourceAvailableItem ResourceItemPrefab;
    public RectTransform ResourceHolder;

    public UICenterLeft UICenterLeft;
}
