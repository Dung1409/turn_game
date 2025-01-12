using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI N_turns , Clock;
    [SerializeField] float minutes;
    [SerializeField] int Quantity;
    int t;
    int n_turns => t;
    int m, s;
 
    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("t"));
        N_turns.text = n_turns.ToString();
        Task task = TCoolDwonTime();
    }


    private void Update()
    {
        string txt = string.Format("{0:00} : {1:00}", m, s);
        N_turns.text = n_turns.ToString();
        Clock.text = txt;   
    }
    
    public void CoolDownTime()
    {
        int x = (int)(t * minutes * 60);
        while (t < Quantity)
        {
            Thread.Sleep(1000);
            x += 1;
            m = (int) (x / 60);
            s = (int) (x % 60);
            if(x == minutes * 60)
            {
                t += 1;
                Debug.Log(t);
                PlayerPrefs.SetInt("t", t);
                x = 0;
            }
        }
    }
    

    public async Task TCoolDwonTime()
    {
        Task task = new Task(() => CoolDownTime());
        task.Start();
        await task;
    }
}
