using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;

public class AnimalUnitComponent : UnitComponent
{
    public AnimalUnit Unit
    {
        get
        {
            return (AnimalUnit)this.baseUnit;
        }
        set
        {
            baseUnit = value;
        }
    }
}
