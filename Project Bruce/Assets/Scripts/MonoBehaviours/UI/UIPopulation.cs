using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bruce;

public class UIPopulation : UI
{
    public override void Initialize(Action SetterCallback)
    {
        base.Initialize(SetterCallback);


        if (SelectedPopulation == null)
        {
            return;
        }

        foreach(Population subPop in SelectedPopulation.subPops)
        {
            var subPopText = Instantiate(MenuObjectPrefab, PopHolder.transform);
            subPopText.ObjectText.text = subPop.rep.ToString();
            Prefabs.Add(subPopText.gameObject);

            foreach (Pop pop in subPop.Pops)
            {
                MenuCountObject menuObject = Instantiate(MenuObjectPrefab, PopHolder.transform);
                menuObject.ObjectText.text = pop.name;
                menuObject.CountText.text = string.Format("{0}", pop.age.ToString());
                Prefabs.Add(menuObject.gameObject);
            }
        }
    }
     
    private void Update()
    {

    }
    public MenuCountObject MenuObjectPrefab;
    public RectTransform PopHolder;
}
