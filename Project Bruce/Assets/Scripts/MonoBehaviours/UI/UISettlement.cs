using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bruce;
public class UISettlement : UI
{

    public override void Initialize(Action SetterCallback)
    {
        base.Initialize(SetterCallback);
        if(SelectedSettlement == null)
        {
            return;
        }
        var resourceDictionary = SelectedSettlement.Stockpile.Resources;
        int housing = 0;

        foreach (Building building in SelectedSettlement.Buildings)
        {
            MenuCountObject buildingMenuObject = Instantiate(MenuObjectPrefab, BuildingHolder);
            buildingMenuObject.ObjectText.text = string.Format(building.name);

            Prefabs.Add(buildingMenuObject.gameObject);

            housing += building.Housing;
        }

        MenuCountObject housingMenuObject = Instantiate(MenuObjectPrefab, ResourceHolder);
        housingMenuObject.ObjectText.text = string.Format("Housing");
        housingMenuObject.CountText.text = string.Format(housing.ToString());
        Prefabs.Add(housingMenuObject.gameObject);

        foreach (Resource resource in resourceDictionary.Keys)
        {
            MenuCountObject resourceMenuObject = Instantiate(MenuObjectPrefab, ResourceHolder);
            resourceMenuObject.ObjectText.text = string.Format(resource.name);
            resourceMenuObject.CountText.text = string.Format(resourceDictionary[resource].ToString());

            Prefabs.Add(resourceMenuObject.gameObject);
        }

        var jobDict = SelectedSettlement.JobManager.JobDictionary;
        foreach(Job job in jobDict.Keys)
        {
            MenuCountObject jobMenuObject = Instantiate(MenuObjectPrefab, JobHolder);
            jobMenuObject.ObjectText.text = string.Format(job.name);
            jobMenuObject.CountText.text =/* jobDict[job].Count > 0 ?*/ string.Format("Worked {0}", jobDict[job].Count); /*: string.Format("Not Worked");*/ 

            Prefabs.Add(jobMenuObject.gameObject);
        }

        ExpandTerritoryButton.onClick.AddListener(() => { ExpandTerritoryButtonOnClick(); });
    }

    public RectTransform ResourceHolder;
    public RectTransform BuildingHolder;
    public RectTransform JobHolder;
    public MenuCountObject MenuObjectPrefab;
    public Button ExpandTerritoryButton;

    public void ExpandTerritoryButtonOnClick()
    {
        switch (MouseController.Instance.mouseMode)
        {
            case MouseMode.Select:
                MouseController.Instance.BeginExpandTerritoryMode(SelectedSettlement);
                break;
            case MouseMode.ExpandTerritory:
                MouseController.Instance.EndExpandTerritoryMode(SelectedSettlement);
                break;
            default:
                MouseController.Instance.EndExpandTerritoryMode(SelectedSettlement);
                break;
        }
    }
}
