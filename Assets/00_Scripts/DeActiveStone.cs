using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActiveStone : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(DeActive());
    }

    IEnumerator DeActive()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);    
    }
}
