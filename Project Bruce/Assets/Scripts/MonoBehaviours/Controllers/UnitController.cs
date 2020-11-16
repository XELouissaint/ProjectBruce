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
        
        UnitToComponentDictionary = new Dictionary<MapUnit, UnitComponent>();
    }

    public static UnitController Instance;

    Dictionary<MapUnit, UnitComponent> UnitToComponentDictionary;
    public UnitComponent UnitPrefab;
    public void OnUnitCreated(MapUnit unit)
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
            unitComp.Unit = unit;
            UnitToComponentDictionary[unit] = unitComp;
        }
        else
        {
            UnitToComponentDictionary[unit].transform.localPosition = unit.CurrentHex.Position;
        }
    }

    void OnUnitMoving(MapUnit unit)
    {

    }

    void OnUnitMoved(MapUnit unit)
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
