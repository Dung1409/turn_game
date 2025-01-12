using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellMana : SupportItem
{

    protected override void Start()
    {
        base.Start();
        string s = "Mana";
        GetItem(s, 1);
        cooldownTimer = 2f;
    }
    public override void Use()
    {
        base.Use(); 
        Debug.Log("Hell 30 mp");
        GameManager.intant.updatePMP(-30);
    }
}
