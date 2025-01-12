using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager> 
{
    public int hero = 0;
    public CharacterDataBase CharacterDB;
    [SerializeField] Transform spawnHeroPos;

    private void Start()
    {
        hero = !PlayerPrefs.HasKey("Hero") ? 0 : PlayerPrefs.GetInt("Hero");
        SpawnHero();
    }
    public Character GetCharacter(string name )
    {
        foreach(Character c in CharacterDB.Characters)
        {
            if (name.Contains(c.name)) return c;
        }
        Character x = new Character(name);
        CharacterDB.Characters.Add(x);
        return x;
    }

    public void SpawnHero()
    {
        GameObject g = Instantiate(CharacterDB.Characters[hero].PlayerPrefabs);
        g.gameObject.transform.position = spawnHeroPos.position;
    }

    public Sprite getIcon()
    {
        return CharacterDB.Characters[hero].Icon;
    }
}
