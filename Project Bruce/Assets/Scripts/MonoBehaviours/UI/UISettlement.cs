using System;
using System.Linq;
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

        UIController.DisplayUI(UICenterLeft,null);
        var resourceDictionary = SelectedSettlement.Stockpile.Resources;
        int housing = 0;

        foreach (Hex hex in SelectedSettlement.Territory)
        {
            foreach (Building building in hex.BuildingManager.ConstructedBuildings)
            {
                MenuCountObject buildingMenuObject = Instantiate(MenuObjectPrefab, BuildingHolder);
                buildingMenuObject.ObjectText.text = string.Format(building.name);
                buildingMenuObject.CountText.text = string.Empty;
                Prefabs.Add(buildingMenuObject.gameObject);

                housing += building.Housing;

            }

            foreach (BuildingPrototype prototype in World.Instance.BuildingManager.UnderConstructionBuildings.Where(p => p.hex == hex))
            {
                MenuCountObject buildingMenuObject = Instantiate(MenuObjectPrefab, BuildingHolder);
                buildingMenuObject.ObjectText.text = string.Format(prototype.building.name);
                buildingMenuObject.CountText.text = prototype.timer.ToString();
                Prefabs.Add(buildingMenuObject.gameObject);
            }
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

        Action setterCallback = () => {  };
        
        ExpandTerritoryButton.onClick.AddListener(() => { ExpandTerritoryButtonOnClick(); });
        OpenJobsListButton.onClick.AddListener(() => { UIController.DisplayUI(UIJobsList, setterCallback); });
        
    }

    public override void Close()
    {
        UIController.HideUI(UICenterLeft);
    }

    public UICenterLeft UICenterLeft;
    public UIJobsList UIJobsList;

    public RectTransform ResourceHolder;
    public RectTransform BuildingHolder;
    public MenuCountObject MenuObjectPrefab;
    public Button ExpandTerritoryButton;
    public Button OpenJobsListButton;

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
