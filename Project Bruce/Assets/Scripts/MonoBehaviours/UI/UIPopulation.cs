using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class UIPopulation : UI
{
    public  void Initialize(Population population)
    {
        base.Initialize();
        this.population = population;



        foreach(Pop pop in population.Pops)
        {
            MenuCountObject menuObject = Instantiate(MenuObjectPrefab, PopHolder.transform);
            menuObject.ObjectText.text = pop.name;
            menuObject.CountText.text = string.Format("{0}", pop.age.ToString());
            Prefabs.Add(menuObject.gameObject);
        }
    }

    public Population population;
    public MenuCountObject MenuObjectPrefab;
    public RectTransform PopHolder;
}
