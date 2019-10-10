using UnityEngine;
using System.Collections;
using System;


// Direction key control

public enum DirectionType
{
    None, Left, Right, Down, Up
}



public delegate void InputEventHandler(DirectionType res = DirectionType.None);


public class CtrlInput  {
    
    
    public InputEventHandler handler;
    
   
    Vector3 m_ptStart = Vector3.zero;

    
    float m_TimeStart;


    float m_Len = 30;

    
    bool m_Flag = false;


   
    public void Start(Vector3 pt)
    {
       
        m_ptStart = pt;
        m_TimeStart = Time.fixedTime;

        m_Flag = true;
    }

    
    public void Check(Vector3 pt)
    {
        
        if (!m_Flag) return;
        
        if (Time.fixedTime - m_TimeStart > 3) return;
        
        Vector3 v = pt - m_ptStart;
        float len = v.magnitude;
     
        if (len < m_Len) return;
       
        float degree = Mathf.Rad2Deg * Mathf.Atan2(v.x, v.y);
        if (-45 >= degree && degree >= -135)
        {   
            m_Flag = false;
            handler(DirectionType.Left);
        }
        else if (45 <= degree && degree <= 135)
        {  
            m_Flag = false;
            handler(DirectionType.Right);

        }
        else if (-45 <= degree && degree <= 45)
        {   

            m_Flag = false;
            handler(DirectionType.Up);

        }
        else if (135 <= degree || degree <= 135)
        {   
            m_Flag = false;
            handler(DirectionType.Down);
        }
    }

}


