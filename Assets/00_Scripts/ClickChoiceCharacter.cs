using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickChoiceCharacter : MonoBehaviour
{
    private void OnMouseDown()
    {
        SelectionPlayer.intant.CurrentCharacter(this.gameObject);
    }
}
