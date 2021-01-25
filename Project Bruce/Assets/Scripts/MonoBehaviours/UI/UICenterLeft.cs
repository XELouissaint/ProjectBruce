using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICenterLeft : UI
{

    public override void Initialize(Action SetterCallback)
    {
        base.Initialize(SetterCallback);
    }

    public UI UIHex;
    public UI UISettlement;

    public Button HexButton;
    public Button SettlementButton;

    public void HideExcept(UI panel)
    {
        if(panel == UIHex)
        {
            UIController.HideUI(UISettlement);
        }
        if(panel == UISettlement)
        {
            UIController.HideUI(UIHex);
        }
    }

    public void ShowUI(UI panel)
    {
        UIController.DisplayUI(panel, null);
        HideExcept(panel);
    }
}
