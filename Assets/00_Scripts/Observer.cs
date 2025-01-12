using System;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    static  Dictionary<string , List<Action>> Listener = new Dictionary<string , List<Action>>();   

    public static void AddListenner(string name , Action callback)
    {
        if (!Listener.ContainsKey(name))
        {
            Listener.Add(name , new List<Action>());
        }
        Listener[name].Add(callback);   
    }

    public static void RemoveListener(string name,Action calllback)
    {
        if (!Listener.ContainsKey(name))
        {
            return;
        }
        Listener[name].Remove(calllback);
    }

    public static void Notify(string name)
    {
        if (!Listener.ContainsKey(name))
        {
            return;
        }
        foreach (var item in Listener[name]) 
        {
            try
            {
                item?.Invoke();
            }
            catch(Exception e) {
                Debug.Log("error");
            }
        }
    }
}
