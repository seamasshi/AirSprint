using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    public GameObject FocusedEnemy;
    [Tooltip("ratio of focus point between player and enemy")]
    public float focusRatio = 0.3f;
    [Tooltip("percentage / second to the target; 1-immediately")]
    public float cameraMovingSpeed = 3.0f;
    public Vector3 targetFocus;
    public float cameraViewSize = 5.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTargetFoucs();
        MoveTowardsFocus();
	
	}

    /// <summary>
    /// calculate a proper focus point and size for the camera
    /// </summary>
    public void UpdateTargetFoucs()
    {
        targetFocus = Vector3.Lerp(player.transform.position,FocusedEnemy.transform.position, focusRatio);
        cameraViewSize = Vector3.Magnitude(player.transform.position - FocusedEnemy.transform.position);
        if (cameraViewSize < 5)
            cameraViewSize = 5;
    }
    /// <summary>
    /// gradually move camera to focus point
    /// </summary>
    public void MoveTowardsFocus()
    {

        Vector3 pos = targetFocus;
        pos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * cameraMovingSpeed);
        transform.GetComponent<Camera>().orthographicSize = Mathf.Lerp(transform.GetComponent<Camera>().orthographicSize,cameraViewSize, Time.deltaTime * cameraMovingSpeed);
    }
    /// <summary>
    /// force the camera move to target postion immediately
    /// </summary>    
    public void FocusImmediately()
    {


    } 
}
