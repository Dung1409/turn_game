using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : SupportItem
{
    protected override void Start()
    {
        base.Start();   
        string s = "Item_Bomb";
        GetItem(s, 2);
        cooldownTimer = 3f;
    }

    public override void Use()
    {
        if(n_uses > 0 && !coolDown )
        {
            ThorwBomb.intant.Throw();
        }
        base.Use();
        
    }

}
