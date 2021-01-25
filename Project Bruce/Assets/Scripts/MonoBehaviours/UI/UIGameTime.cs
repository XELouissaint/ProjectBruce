using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bruce;
public class UIGameTime : UI
{
    public override void Initialize(Action SetterCallback)
    {
        base.Initialize(SetterCallback);
        //string dateString = World.Instance.CalendarSystem.DateString();

        //DateText.text = dateString;
        
    }

    public Text DateText;
}
