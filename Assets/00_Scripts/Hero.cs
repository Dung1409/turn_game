using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public Dictionary<string , List<GameObject>> skill = new Dictionary<string, List<GameObject>>();

    public abstract void Skill_1();

    public abstract void Skill_2();

}
