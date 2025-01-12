using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThorwBomb : Singleton<ThorwBomb>   
{
    [SerializeField] Transform startPoint, endPoint;
    [SerializeField] int Angle = 0 , Trajectory_number = 100;
    float Angle_Rad = 0; 
    [SerializeField] float V = 0 , config ;
    [SerializeField] GameObject bomb , g;
    void CalV()
    {
        float y = endPoint.transform.position.y - startPoint.transform.position.y;
        float x = endPoint.transform.position.x - startPoint.transform.position.x;
        if (x < 0 )
        {   
            Angle_Rad = -Mathf.Abs(Angle) * Mathf.Deg2Rad;
            config = -Mathf.Abs(config);
        }
        else
        {
            Angle_Rad = Mathf.Abs(Angle) * Mathf.Deg2Rad;
            config = Mathf.Abs(config);
        }
        float v2  = (10 / (( Mathf.Tan(Angle_Rad) * x - y ) / (x * x) )) / (2 * Mathf.Cos(Angle_Rad) * Mathf.Cos(Angle_Rad));
        v2 = Mathf.Abs(v2);
        V = Mathf.Sqrt(v2);
    }
    public void Throw()
    {
        CalV();
        if(g == null)
        {
            g = Instantiate(bomb, startPoint.transform.position, Quaternion.identity);
        }
        else
        {
            g.SetActive(true);
            g.transform.position = startPoint.transform.position;
        }
        
        Vector3 force = Vector3.zero;
        force.x = V * 50 * Mathf.Cos(Angle_Rad);
        force.y = V * 50 * Mathf.Sin(Angle_Rad);
        Debug.Log(force);
        g.GetComponent<Rigidbody2D>().AddForce(force * Mathf.Sign(config));
        g.GetComponent<Rigidbody2D>().velocity *= 2;
    }
    private void OnDrawGizmosSelected()
    { 
        CalV();
        Gizmos.color = Color.red;
        for (int i = 0; i < Trajectory_number; i++)
        {
            float t = i * config;
            float x = V * Mathf.Cos(Angle_Rad) * t; //x = x0 + v0 *cos(a) * t
            float y = V * Mathf.Sin(Angle_Rad)* t - 0.5f * 10 * t * t; // y = yo + v0 * sin(a) - 1/2 * g * t ^2
            Vector3 pos1 = startPoint.transform.position + new Vector3 (x, y, 0);

            t = (i + 1) * config;
            x = V * Mathf.Cos(Angle_Rad) * t; 
            y = V * Mathf.Sin(Angle_Rad) * t - 0.5f * 10 * t * t;
            Vector3 pos2 = startPoint.transform.position + new Vector3(x, y, 0);

            Gizmos.DrawLine(pos1, pos2);
        }
    }
    
  
}
