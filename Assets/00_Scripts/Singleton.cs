using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _intant;
    public static T intant => _intant;

    private void Awake()
    {
        if ( _intant == null)
        {
            _intant = this as T ;
        }
        else
        {
            Destroy(this as T);
        }
    }
}
