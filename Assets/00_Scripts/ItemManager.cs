using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public Sprite img;
    public string description;
    public int Physical_damage;
    public int Magical_damge;
    public int mana;
    public int hell;
    public int armor;

}
public class ItemManager : MonoBehaviour
{
  

   [SerializeField] List<Item> items = new List<Item>();

    private static ItemManager _intant;
    public static ItemManager intant => _intant;


    private void Start()
    {
        if(_intant == null)
        {
            _intant = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public Item info(string name)
    {
        foreach(Item x in items)
        {
            if(x.img.name == name)
            {
                return x;
            }
        }
        return null;
    }
}
