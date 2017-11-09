using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    //delegates
    /// <summary>
    /// delegate of sweep action
    /// </summary>
    /// <param name="dir"> direction of sweep</param>
    /// <param name="mag"> length of sweep </param>
    public delegate void InputDelegateSweep(Vector2 dir);
    public static event InputDelegateSweep inputDelegate_Sweep;
    /// <summary>
    /// delegate of hold action - press ... don't release
    /// </summary>
    public delegate void InputDelegateHold();
    public static event InputDelegateHold inputDelegate_StartHold;
    public static event InputDelegateHold inputDelegate_EndHold;
    /// <summary>
    /// delegate of click action
    /// </summary>
    /// <param name="pos">click position    </param>
    public delegate void InputDelegateClick(Vector2 pos);
    public static event InputDelegateClick inputDelegate_Click;
    /// <summary>
    /// delegate of drag action
    /// </summary>
    /// <param name="dir">direction from start position toward current position</param>
    /// <param name="mag">distance from start position to current position</param>
    public delegate void InputDelegateDrag(Vector2 dir, float mag);
    public static event InputDelegateDrag inputDelegate_Drag;




  

    Vector2 lastClickDownPosition;
    Vector2 lastClickUpPosition;
    Vector2 lastHoldPosition;
    float lastClickDownTime;
    float lastClickUpTime;
    float lastHoldTime;

    bool clickHold = false;
    bool isHoldDelegateBroadcasted = false;
	
	void Start () {

       
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Fire1Down");
            lastClickDownPosition = Input.mousePosition;
            lastClickDownTime = Time.time;
            clickHold = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            //Debug.Log("Fire1Down");
            lastClickUpPosition = Input.mousePosition;
            lastClickUpTime = Time.time;
            clickHold = false;
            CheckHoldEnd();

            CheckSweepOrClick();
        }

        if (clickHold)
        {
            lastHoldPosition = Input.mousePosition;
            lastHoldTime = Time.time;
            CheckDrag();
        }
        CheckHoldStart();
    }

    
    void CheckSweepOrClick()
    {
        Vector2 wholeVector = lastClickUpPosition - lastClickDownPosition;
        float period = lastClickUpTime - lastHoldTime;
        float speed = (lastClickUpPosition - lastHoldPosition).magnitude / period;
        if (speed > 600.0f && wholeVector.magnitude > 200)
        {
            Vector2 vec = lastClickUpPosition - lastHoldPosition;
            vec.Normalize();
            inputDelegate_Sweep(vec);
        }
        else
        {
            if (lastClickUpTime-lastClickDownTime < 0.3f)
                inputDelegate_Click(lastClickUpPosition);

        }
    }

    void CheckHoldStart()
    {
        if (clickHold)
        {
            if (Time.time - lastClickDownTime > 0.15f )
            {
                if (!isHoldDelegateBroadcasted)
                {
                    inputDelegate_StartHold();
                    isHoldDelegateBroadcasted = true;
                }
                
            }
        }
    }

    void CheckHoldEnd()
    {
        if (isHoldDelegateBroadcasted)
        {
            isHoldDelegateBroadcasted = false;
            inputDelegate_EndHold();
        }        
    }

    void CheckDrag()
    {
        Vector2 dir = lastHoldPosition - lastClickDownPosition;
        float mag = (lastHoldPosition - lastClickDownPosition).magnitude;
        dir.Normalize();
        if (mag > 30 && (lastHoldTime - lastClickDownTime > 0.12f))
        {
            inputDelegate_Drag(dir, mag);

        }

    }
}
