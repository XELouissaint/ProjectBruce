using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class UnitController : MonoBehaviour
{
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

    }
    public void Init()
    {
        
        UnitToComponentDictionary = new Dictionary<Unit, UnitComponent>();
    }

    public static UnitController Instance;

    Dictionary<Unit, UnitComponent> UnitToComponentDictionary;
    public UnitComponent UnitPrefab;
    public void OnUnitCreated(Unit unit)
    {
        unit.RegisterOnMoved(OnUnitMoved);
        Debug.Log("OnUnitCreated");
        if (unit == null)
        {
            return;
        }

        if (UnitToComponentDictionary.ContainsKey(unit) == false)
        {
            UnitComponent unitComp = Instantiate(UnitPrefab, this.transform);
            unitComp.transform.localPosition = unit.CurrentHex.Position;
            UnitToComponentDictionary[unit] = unitComp;
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
        if (UnitToComponentDictionary.ContainsKey(unit) == false)
        {
            OnUnitCreated(unit);
        }
        else
        {
            UnitToComponentDictionary[unit].MoveToHex(unit.CurrentHex);
        }
    }
}
