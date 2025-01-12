using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Wizard : Hero
{
    [SerializeField] GameObject FireBall;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject Stone;
    GameObject g1 , g2;
    private void Start()
    {
        skill.Add("skill_1", new List<GameObject>());
    }
    public override void Skill_1()
    {
        if (g1 == null) g1 = Instantiate(FireBall);
        else if (g1 != null || !g1.activeSelf)
            g1.SetActive(true);
        g1.transform.position = shootPoint.position;

    }

    public override void Skill_2()
    {
        if (g2 == null) g2 = Instantiate(Stone);
        else if (g2 != null || !g2.activeSelf)
            g2.SetActive(true);
        g2.transform.position = GameManager.intant.EnermyAnim.transform.position;
    }


}
