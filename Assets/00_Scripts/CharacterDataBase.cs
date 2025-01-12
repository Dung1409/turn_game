using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Character DataBase")]
public class CharacterDataBase : ScriptableObject
{
   public List<Character> Characters = new List<Character>();
}
[Serializable]
public class Character
{
    public Sprite Icon;
    public string name;
    public int HP, MP;
    public int damage;
    public GameObject PlayerPrefabs;
    public List<Sprite> Skills = new List<Sprite>();
    public List<Sprite> Items = new List<Sprite>(); 

    public Character(string name)
    {
        this.name = name;
        this.HP = 200;
        this.MP = 150;
        this.damage = 10;
    }
    
}