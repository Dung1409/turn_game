using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SupportItem : MonoBehaviour, IPointerClickHandler
{
    public float cooldownTimer, timer;
    public bool coolDown;
    public int n_uses;
    public Text quantity;
    public Image icon;
    protected virtual void Start()
    {
        icon = this.GetComponent<Image>();
        quantity = this.GetComponentInChildren<Text>();
        coolDown = false;
    }
    public void  OnPointerClick(PointerEventData eventData)
    {   
        Use();
        
    }

    public virtual void Use()
    {
        if(n_uses == 0 || coolDown)
        {
            return;
        }
        n_uses--;
        quantity.text = quantity.text = n_uses.ToString();
        icon.fillAmount = 0.01f;
        coolDown = true;
    }

    public void Update()
    {
        if(icon.fillAmount < 1f)
        {
            timer += Time.deltaTime;
            icon.fillAmount = timer / cooldownTimer;
            if(timer >= cooldownTimer)
            {
                icon.fillAmount = 1f;
                timer = 0f;
                coolDown = false;
                return;
            }
        }
    }

    public virtual void GetItem(string i , int x)
    {
        n_uses = !PlayerPrefs.HasKey(i) ? x : PlayerPrefs.GetInt(i);
        quantity.text = n_uses.ToString();
    }

}
