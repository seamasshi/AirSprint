using UnityEngine;
using System.Collections;
using UnityEngine.Events;

//[System.Serializable]
//public class InputSweepEvent : UnityEvent<Vector2, float>
//{
//}

public class InputManager : MonoBehaviour {

    //delegates
    /// <summary>
    /// delegate of sweep action
    /// </summary>
    /// <param name="dir"> direction of sweep</param>
    /// <param name="mag"> length of sweep </param>
    public delegate void InputDelegateSweep(Vector2 dir, float mag);
    public static event InputDelegateSweep inputDelegate_Sweep;
    /// <summary>
    /// delegate of hold action - press ... don't release
    /// </summary>
    public delegate void InputDelegateHold();
    public static event InputDelegateHold inputDelegate_StartHold;
    public static event InputDelegateHold inputDelegate_EndHold;

    




   // public InputSweepEvent inputSweepEvent;

    Vector2 lastClickDown;
    Vector2 lastClickUp;
    float lastClickDownTime;
    float lastClickUpTime;

    
	// Use this for initialization
	void Start () {

       // if (inputSweepEvent == null)
       //     inputSweepEvent = new InputSweepEvent();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Fire1Down");
            lastClickDown = Input.mousePosition;
            lastClickDownTime = Time.time;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            //Debug.Log("Fire1Down");
            lastClickUp = Input.mousePosition;
            lastClickUpTime = Time.time;
            CheckSweep();
        }
    }

    
    void CheckSweep()
    {
        Vector2 vec = lastClickUp - lastClickDown;
        float mag = vec.magnitude;
        vec.Normalize();
        float period = lastClickUpTime - lastClickDownTime;
        if (mag > 80 && period < 0.4f)
        {
            //Debug.Log("Sweep Action");
            inputDelegate_Sweep(vec, mag);
        } 
    }
}
