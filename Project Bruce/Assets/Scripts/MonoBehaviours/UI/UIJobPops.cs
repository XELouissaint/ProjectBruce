using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bruce;

public class UIJobPops : UI
{
    public MenuCountObject MenuObjectPrefab;
    public RectTransform popHolder;
    public Text MainText;

    public List<Pop> Pops = new List<Pop>();
    public override void Initialize(Action SetterCallback)
    {
        base.Initialize(SetterCallback);

        foreach (Pop pop in Pops)
        {
            MenuCountObject menuObject = Instantiate(MenuObjectPrefab, popHolder.transform);

            menuObject.ObjectText.text = pop.name;
            menuObject.CountText.text = string.Empty;

            Prefabs.Add(menuObject.gameObject);
        }
    }
}
