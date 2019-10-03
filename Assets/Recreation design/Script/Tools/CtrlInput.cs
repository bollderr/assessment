using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Direction key control
/// </summary>
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

    #region 示例
    /*使用方法1：实例化 CtrlInput 类，
     * 实例化对象名：如 ctrlInput
     * 用新实例化出来的对象，调用方法
     * ctrlInput.TouchControl();
     * ctrlInput.MouseControl();
     * 
     * 使用方法2：实例化 CtrlInput 类，
     * 将方法拷贝或剪切到 控制类中
     * 修改示例中的 Start(touch.position),Check(touch.position) 方法调用为
     * ctrlInput.Start(touch.position);
     * ctrlInput.Check(touch.position);
     */
    #region 1、移动端触点移动示例
    /// <summary>
    /// 触点输入控制
    /// </summary>
    public void TouchControl()
    {
        //如果触点数量为0返回
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Start(touch.position);
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Check(touch.position);
        }
    }
    #endregion

    #region 2、PC端鼠标输入移动示例
    
    bool m_MouseMove = false;
    /// <summary>
    /// 鼠标输入控制
    /// </summary>
    void MouseControl()
    {
        //鼠标按下时记录当前位置
        if (Input.GetMouseButtonDown(0))
        {
            m_MouseMove = true;
            Start(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_MouseMove = false;
        }
        //滑动位置
        if (m_MouseMove)
        {
            Check(Input.mousePosition);
        }
    }
    #endregion
    #endregion


}


