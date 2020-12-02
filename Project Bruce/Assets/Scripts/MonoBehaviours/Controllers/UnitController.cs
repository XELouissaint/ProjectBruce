using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class UnitController : MonoBehaviour
{
    private void Awake()
    {
        

    }
    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        UnitToComponentDictionary = new Dictionary<Unit, UnitComponent>();
    }

    public static UnitController Instance;

    Dictionary<Unit, UnitComponent> UnitToComponentDictionary;
    public PopUnitComponent UnitPrefab;
    public AnimalUnitComponent AnimalPrefab;
    public void OnUnitCreated(Unit unit)
    {
        unit.RegisterOnMoved(OnUnitMoved);

        if (unit == null)
        {
            return;
        }

        if (UnitToComponentDictionary.ContainsKey(unit) == false)
        {
            if(unit is PopUnit popUnit)
            {
                PopUnitComponent unitComp = Instantiate(UnitPrefab, this.transform);
                unitComp.transform.localPosition = unit.CurrentHex.Position;
                unitComp.Unit = popUnit;
                UnitToComponentDictionary[unit] = unitComp;
            }else if (unit is AnimalUnit animalUnit)
            {
                AnimalUnitComponent unitComp = Instantiate(AnimalPrefab, this.transform);
                unitComp.transform.localPosition = unit.CurrentHex.Position;
                unitComp.Unit = animalUnit;
                UnitToComponentDictionary[unit] = unitComp;
            }
        }
        else
        {
            UnitToComponentDictionary[unit].transform.localPosition = unit.CurrentHex.Position;
        }
    }

    void OnUnitMoving(Unit unit)
    {

    }

    void OnUnitMoved(Unit unit)
    {
        Debug.Log("MOVE");
        if (UnitToComponentDictionary.ContainsKey(unit) == false)
        {
            OnUnitCreated(unit);
        }
        else
        {
            UnitToComponentDictionary[unit].BeginMoveToHex(unit.CurrentHex);
        }
    }
}
