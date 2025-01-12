using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    Dictionary<string , GameObject[] > obj = new Dictionary<string, GameObject[]> ();
    public Animator PlayerAnim , EnermyAnim;
    public EnermyState enermyState;
    public bool hit;
    Character player , enermy;
    [Header("-----------Countdown------------")]
    [SerializeField] List<Sprite> Clock = new List<Sprite>();
    [SerializeField] Image countDown;

    [Header("----------Item Bar--------------")]
    [SerializeField] GameObject ItemBar;
    [SerializeField] bool youTurn;

    [Header("--------PlAYER ATTRIBUTE--------")]
    [SerializeField] Image Icon;
    [SerializeField] int PlayerHP;
    [SerializeField] int PlayerMP;
    [SerializeField] int Pdamage;
    [SerializeField] Image PHP, PMP;


    [Header("--------ENERMY ATTRIBUTE--------")]
    [SerializeField] int EnermyHP;
    [SerializeField] int EnermyMP;
    [SerializeField] int Edamage;
    [SerializeField] Image EHP, EMP;
    int count = 0;

    [Header("----------Skill Effect----------")]
    [SerializeField] GameObject explode;

    [SerializeField] ParticleSystem ParticleSystem;

    UnityEvent EnermyEvent = new UnityEvent();
    void Start()
    {   
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        obj.Add("Player", p);
        GameObject[] q = GameObject.FindGameObjectsWithTag("Enermy");
        obj.Add("Enermy", q);

        p = null;
        if(obj.TryGetValue("Player" , out p))
        {
            //....lay ra player
            PlayerAnim = p[0].GetComponent<Animator>();
        }
        Icon.sprite = CharacterManager.intant.getIcon();   
        q = null;
        if (obj.TryGetValue("Enermy", out q))
        {
            EnermyAnim = q[0].GetComponent<Animator>();
        }
        youTurn = true;
        //"------------Player-----------"
        player = CharacterManager.intant.GetCharacter(PlayerAnim.gameObject.name);
        PlayerHP = player.HP;
        PlayerMP = player.MP;
        Pdamage = player.damage;
        //"-----------Enermy------------"
        enermy = CharacterManager.intant.GetCharacter(EnermyAnim.gameObject.name);
        EnermyHP = enermy.HP;
        EnermyMP = enermy.MP;
        Edamage = enermy.damage;
        //"-------countdown-------------"
        countDown.gameObject.SetActive(false);
        explode.SetActive(false);
        ParticleSystem.gameObject.SetActive(false); 

        Observer.AddListenner("EndGame", EndGame);
        hit = true;
    }

    private void Update()
    {

        if (youTurn == false)
        {
            youTurn = true;
            StartCoroutine(EnermyAttack()); 
        }
        Observer.Notify("EndGame");
    }

    private void OnDestroy()
    {
        Observer.RemoveListener("EndGame", EndGame);
        
    }
    public enum PlayerState
    {
        Idle,
        Skill_1,
        Skill_2,
        hit
    }

    public enum EnermyState
    {
        Idle,
        Skill_1,
        Skill_2,
        hit
    }
    //"--------Skill Player--------"
    IEnumerator Player_Ilde()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerAnim.SetInteger("State", (int) PlayerState.Idle);
    }

    public void Player_Skill_1(int MP , int bonus)
    {
        if(PlayerMP - MP < 0)
        {
            return;
        }

        PlayerAnim.SetInteger("State", (int)PlayerState.Skill_1);
        updatePMP(MP);
        youTurn = false;
        ItemBar.SetActive(youTurn);
        StartCoroutine(Player_Ilde());
        Enermy_Hit(bonus);
    }

    public void Player_Skill_2(int MP , int bonus) 
    {
        if (PlayerMP - MP <= 0)
        {
            return;
        }
        PlayerAnim.SetInteger("State", (int)PlayerState.Skill_2);
        updatePMP(MP);
        youTurn = false;
        ItemBar.SetActive(youTurn);
        StartCoroutine(Player_Ilde());
        Enermy_Hit(bonus);
        
    }

    public void Player_hit() 
    {
        StartCoroutine(Explode(PlayerAnim.transform));
        PlayerAnim.SetInteger("State", (int)PlayerState.hit);
        StartCoroutine(Player_Ilde());
        
    }

    public void Helling(int hell)
    {
        StartCoroutine(Hell(15 , 3));
        youTurn = false;
        ItemBar.SetActive(youTurn);
         
    }

    IEnumerator Hell(int hell , int h_seconds)
    {
        ParticleSystem.gameObject.SetActive(true);
        ParticleSystem.gameObject.transform.position = PlayerAnim.transform.position;
        for(int i = 1; i <= h_seconds ; i ++)
        {
            yield return new WaitForSeconds(1);
            PlayerHP = Mathf.Min(100, PlayerHP + hell / h_seconds );
            PHP.fillAmount = (float)PlayerHP / 100;
        }
        ParticleSystem.gameObject.SetActive(false);
    }

    //"--------Skill Enermy--------"
    IEnumerator Enermy_Idle()
    {
        yield return new WaitForSeconds(0.5f);
        EnermyAnim.SetInteger("State", (int)EnermyState.Idle);
        enermyState = EnermyState.Idle;
    }

    public void Enermy_Hit(int damage)
    {
        enermyState = EnermyState.hit;
        StartCoroutine(Wait(damage)); 
       
    }
    IEnumerator Wait(int damage)
    {
        yield return new WaitForSeconds(1f);
        EnermyHP = Mathf.Max(0, EnermyHP - Pdamage - damage);
        EHP.fillAmount = (float)EnermyHP / enermy.HP;
        StartCoroutine(Explode(EnermyAnim.transform));
        EnermyAnim.SetInteger("State", (int)EnermyState.hit);
        StartCoroutine(Enermy_Idle());
    }

    public void Enermy_Skill_1()
    {
        EnermyAnim.SetInteger("State", (int)EnermyState.Skill_1);
        enermyState = EnermyState.Skill_1;
        StartCoroutine(Enermy_Idle());
    }

    public void Enermy_Skill_2()
    {
        EnermyAnim.SetInteger("State", (int)EnermyState.Skill_2);
        enermyState = EnermyState.Skill_2;
        StartCoroutine(Enermy_Idle());
    }

    IEnumerator EnermyAttack()
    {
        int bonusD = 0 , mp;
        yield return new WaitForSeconds(1.5f);
        countDown.gameObject.SetActive(true);
        for(int i = 0 ; i < 3; i++)
        {
            countDown.sprite = Clock[i];
            yield return new WaitForSeconds(1f);
        }
        countDown.gameObject.SetActive(false);
        EnermyEvent.AddListener(Enermy_Skill_1);
        bonusD = 5;
        mp = 0;
        count++;
        if(count >= 2 && EnermyMP >= 30)
        {
            EnermyEvent.AddListener(Enermy_Skill_2);
            updataEMP(15);
            bonusD = 10;
            mp = 10;
            count = 0;
        }
        yield return new WaitForSeconds(1f);
        EnermyEvent.Invoke();
        updataEMP(mp);
        Observer.Notify("Armor");
        if (hit)
        {
            getDamge(bonusD);
            Player_hit();
        }
        hit = true;
        yield return new WaitForSeconds(1f);
        countDown.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            countDown.sprite = Clock[i];
            yield return new WaitForSeconds(1f);
        }
        ItemBar.SetActive(youTurn);
        countDown.gameObject.SetActive(false);
    }


    public void getDamge(int damage)
    {
        PlayerHP = Mathf.Max(0 , PlayerHP - Edamage - damage);
        PHP.fillAmount = (float)PlayerHP / player.HP;
    }

    public void updatePMP(int MP)
    {
        if(PlayerHP - MP < 0) 
        {
            return;
        }
        PlayerMP -= MP;
        PMP.fillAmount = (float)PlayerMP / player.MP;
    }

    public void EndGame()
    {
        if(PlayerHP == 0)
        {
            Debug.Log("YOU LOSE");
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
           
        }

        if(EnermyHP == 0) 
        {
            Debug.Log("YOU WIN");
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator Explode(Transform target)
    {
        explode.SetActive(true);
        explode.transform.position = target.position;
        yield return new WaitForSeconds(1f);
        explode.SetActive(false);
    }

    void updataEMP(int MP)
    {
        EnermyMP = Mathf.Max(0 , EnermyMP - MP);
        EMP.fillAmount = (float) EnermyMP / enermy.MP;
    }
}