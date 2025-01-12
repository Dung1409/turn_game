using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionPlayer : Singleton<SelectionPlayer>
{

    [SerializeField] CharacterDataBase characterData;
    public List<GameObject> Hero;

    [Header("-----------Info hero-----------")]
    [SerializeField] private Image Icon;
    [SerializeField] private List<Image> skill;
    [SerializeField] private List<Image> Items;

    public static int currentCharacter = 0; 
    public int previuosCharacter = 0;
    public int indexOfHeroInDB;
    [SerializeField] Vector3 pos;

    private void Start()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        Hero = g.ToList();
        Hero.Sort(delegate(GameObject a , GameObject b) 
        {
            return a.name.CompareTo(b.name);    
        });
        Hero[currentCharacter].GetComponent<SpriteRenderer>().color = Color.white;
        setInfo();
    }
    
    public GameObject Player()
    {
        return Hero[currentCharacter];
    }

    public void CurrentCharacter(GameObject g)
    {

        previuosCharacter = currentCharacter; 
        currentCharacter =  Hero.FindIndex(x  => x.gameObject == g);
        Hero[previuosCharacter].GetComponent<SpriteRenderer>().color = new Color(0.2196079f , 0.2196079f , 0.2196079f , 1f);
        Hero[currentCharacter].GetComponent<SpriteRenderer>().color = Color.white;
        setInfo();
        pos = Hero[currentCharacter].transform.position;
        Debug.Log(pos);
        Hero[currentCharacter].transform.position = Hero[previuosCharacter].transform.position;
        Hero[previuosCharacter].transform.position = pos;
    }

    private void setInfo()
    {
        indexOfHeroInDB = characterData.Characters.FindIndex(
            x => x.name.ToLower() == Hero[currentCharacter].gameObject.name.ToLower());
        Character x = characterData.Characters[indexOfHeroInDB];
        PlayerPrefs.SetInt("Hero", indexOfHeroInDB);
        Icon.sprite = x.Icon;
        for(int i = 0; i < skill.Count ; i++)
        {
            skill[i].sprite = x.Skills[i];
        }
        /*
        foreach (Image item in Items)
        {
            item.sprite = null;
        }

        for (int i = 0; i < x.Items.Count ; i++) 
        {
            Items[i].sprite = x.Items[i];
        }
        */
    }
    
    public void addItemForHero(Sprite item)
    {
        characterData.Characters[indexOfHeroInDB].Items.Add(item);
    }

    public void Play()
    {
        /*
        if(!PlayerPrefs.HasKey("n_turns")||PlayerPrefs.GetInt("n_turns") == 0)
        {
            return;
        }
        int x = PlayerPrefs.GetInt("n_turns") - 1;
        PlayerPrefs.SetInt("n_turns" , x);
        */
        SceneManager.LoadScene(2);
    }
}
