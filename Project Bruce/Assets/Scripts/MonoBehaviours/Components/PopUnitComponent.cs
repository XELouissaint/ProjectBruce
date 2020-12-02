using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bruce;
public class PopUnitComponent : UnitComponent
{
    public PopUnit Unit
    {
        get
        {
            return (PopUnit)this.baseUnit;
        }
        set
        {
            baseUnit = value;
        }
    }
}
