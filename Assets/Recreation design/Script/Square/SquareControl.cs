using UnityEngine;
using System.Collections;
using System;

public class SquareControl : MonoBehaviour {
    
    float timer;//timer
    float stepTime = 0.8f;
    int multiple = 15;
    // mouse move
    bool m_MouseMove = false;
    // deal with input
    CtrlInput m_CtrlInput = new CtrlInput();

    public bool isPause { get; set; }
    public DirectionType dirType { get; set; }
    private void Awake()
    {
        dirType = DirectionType.None;
        m_CtrlInput.handler += onMove;
    }

    void onMove(DirectionType res)
    {
        dirType = res;
    }

    // Update is called once per frame
    void Update () {

        if (isPause) return;
        
        timer += Time.deltaTime;
        if (timer > stepTime)
        {
            timer = 0;
            WhereAbouts();
        }
        InputControl();
        MouseControl();
        TouchControl();
    }

    //IEnumerator doWhereabouts()
    //{
    //    while (true)
    //    {
    //        while (!isPause)
    //        {
    //            Vector3 pos = transform.position;
    //            pos.y -= 1;
    //            transform.position = pos;
    //            if (!GameManager.instance.IsValidMapPosition(transform))
    //            {
    //                AudioManager.instance.PlayDrop();
    //                pos.y += 1;
    //                transform.position = pos;
    //                isPause = true;
    //                bool isLineclear = GameManager.instance.PlaceShape(transform);
    //                if (isLineclear)
    //                {
    //                    AudioManager.instance.PlayLineClear();
    //                }
    //                else
    //                {
    //                    AudioManager.instance.PlayControl();
    //                }

    //                GameManager.instance.FallDown();
    //                Destroy(this);
    //            }
    //            //adjust speed
    //            if (!isSpeedup)
    //            {
    //                yield return new WaitForSeconds(1);
    //            }
    //            else
    //            {
    //                yield return new WaitForSeconds(0.2f);
    //            }
    //        }
    //        yield return null;
    //    }
        
    //    //while (isSpeedup)
    //    //{
    //    //    transform.position = new Vector2(transform.position.x, transform.position.y - 1);
    //    //    yield return new WaitForSeconds(0.5f);
    //    //}
    //}
    void WhereAbouts()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        if (!GameManager.instance.IsValidMapPosition(transform))
        {
            AudioManager.instance.PlayDrop();
            pos.y += 1;
            transform.position = pos;
            isPause = true;
            bool isLineclear = GameManager.instance.PlaceShape(transform);
            if (isLineclear)
            {
                AudioManager.instance.PlayLineClear();
            }
            else
            {
                AudioManager.instance.PlayControl();
            }

            GameManager.instance.FallDown();
            Destroy(this);
        }
    }
    /// <summary>
    /// Keyboard input control
    /// </summary>
    void InputControl() {
        float h = 0;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || dirType == DirectionType.Left)
        {
            h = -1;
            dirType = DirectionType.None;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || dirType == DirectionType.Right)
        {
            h = 1;
            dirType = DirectionType.None;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || dirType == DirectionType.Down)
        {
            stepTime /= multiple;
            dirType = DirectionType.None;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || dirType == DirectionType.Up)
        {
            dirType = DirectionType.None;
            transform.RotateAround(transform.Find("Pivot").transform.position, Vector3.forward, 90);
            if (!GameManager.instance.IsValidMapPosition(transform))
            {
                transform.RotateAround(transform.Find("Pivot").transform.position, Vector3.forward, -90);
            }
            AudioManager.instance.PlayDrop();
        }
        if (h != 0)
        {
            dirType = DirectionType.None;
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if (!GameManager.instance.IsValidMapPosition(transform))
            {
                pos.x -= h;
                transform.position = pos;
            }
            AudioManager.instance.PlayDrop();
        }
    }
    /// <summary>
    /// mouse input control
    /// </summary>
    void MouseControl() {
        //save initial position when mouse clicked
        if (Input.GetMouseButtonDown(0))
        {
            m_MouseMove = true;
            m_CtrlInput.Start(Input.mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            m_MouseMove = false;
        }
        //change position
        if (m_MouseMove)
        {
            m_CtrlInput.Check(Input.mousePosition);
        }
    }
    /// <summary>
    /// touch control
    /// </summary>
    void TouchControl() {
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            m_CtrlInput.Start(touch.position);
        }else if(touch.phase == TouchPhase.Moved)
        {
            m_CtrlInput.Check(touch.position);
        }        
    }

    public void SetColor(Color color)
    {
        foreach (Transform t in transform)
        {
            if (t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}
