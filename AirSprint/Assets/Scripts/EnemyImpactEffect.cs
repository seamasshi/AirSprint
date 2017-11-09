using UnityEngine;
using System.Collections;

public class EnemyImpactEffect : MonoBehaviour {
    //public bool hit = false;
    public float hitPower = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);
        //if (!hit)
        //{
            if (collision.gameObject.tag == "Player")
            {
                //hit = true;
                Vector2 vec = collision.gameObject.transform.position - transform.position;
                vec.Normalize();
                collision.gameObject.GetComponent<PlayerController>().Hit(vec, hitPower);
            }            
         //}
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
