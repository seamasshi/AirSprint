using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    [Tooltip("don't fill, FindGameObjectWithTag")]
    public GameObject player;
    public Image playerEnergyBarFill;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        playerEnergyBarFill.fillAmount = player.GetComponent<PlayerController>().getPlayerEnergyPercentage();
	}
}
