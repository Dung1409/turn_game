using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour
{
    bool colli;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colli = true;
        GameManager.intant.Enermy_Hit(10);
        this.gameObject.SetActive(false);

    }
}
