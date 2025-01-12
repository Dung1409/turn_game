using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private void Update()
    {
        this.transform.Translate(-this.transform.right.normalized / 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);   
    }
}
