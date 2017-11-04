using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ProgressBarShowAmount : MonoBehaviour {
    public float maxValue;
    float value;
    public Image playerEnergyBarFill;
    public string valueName;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        value = playerEnergyBarFill.fillAmount * maxValue;
        GetComponent<Text>().text = valueName + ": " + Mathf.Round(value).ToString() + " / " + maxValue.ToString();
	}
}
