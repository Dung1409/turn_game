using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Armor : SupportItem
{
    [SerializeField] ParticleSystem armor;
   
    protected virtual void Start()
    {
        base.Start();   
        string s = "Item_Bomb";
        GetItem(s, 1);
        armor.gameObject.SetActive(false);
        cooldownTimer = 2f;
        Observer.AddListenner("Armor", CheckTurn);
    }

    public override void Use()
    {
        if (n_uses > 0)
        {
            armor.gameObject.SetActive(true);
            armor.gameObject.transform.position = GameManager.intant.PlayerAnim.transform.position;
        }
        base.Use();
    }

    private void OnDestroy()
    {
        Observer.RemoveListener("Armor" , CheckTurn);
    }
    public void CheckTurn()
    {
        bool u = armor.gameObject.activeSelf;
        GameManager.EnermyState x = GameManager.intant.enermyState;
        if(x.Equals(0) || x.Equals(3))
        {
            return;
        }
        else
        {
            if (u) 
            { 
                GameManager.intant.hit = false;
                StartCoroutine(wait());  
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForEndOfFrame();
        armor.gameObject.SetActive(false);
    }
}

