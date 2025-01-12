using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CilckEvent : MonoBehaviour , IPointerDownHandler
{
    Sprite _sprite;
    private  int hellCount;
    Item x;
    private void Awake()
    {
        _sprite  = GetComponent<Image>().sprite;
        hellCount = 2;
        Observer.AddListenner("Hell", Hell);
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        x = ItemManager.intant.info(_sprite.name);
        int dmg = 0;
        if (x.description.ToLower().Contains("physical attack"))
        {
            dmg = x.Physical_damage;
            GameManager.intant.Player_Skill_1(x.mana , dmg);
            
        }
        else if (x.description.ToLower().Contains("magical attack"))
        {
            dmg = x.Magical_damge;
            GameManager.intant.Player_Skill_2(x.mana , dmg);
            
        }
        else
        {
           Observer.Notify("Hell");
        }
        
    }
    private void OnDestroy()
    {
        Observer.RemoveListener("Hell" , Hell);
    }

    void Hell()
    {
        if(hellCount == 0)
        {
            return;
        }
        hellCount--;
        int hell = x.hell;
        GameManager.intant.Helling(hell);
    }
}
