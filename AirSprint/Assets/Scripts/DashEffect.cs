using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DashEffect : MonoBehaviour {
    public float life = 0.6f;
    public float hitPower = 100;
    public float energyBackflowValue = 25;
    public bool hit = false;
    public List<int> hitObjects = new List<int>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;
        if (life < 0)
            GameObject.Destroy(gameObject);
	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.tag == "Enemy")
        {
            if (!hit)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SkillHit(energyBackflowValue);
                hit = true;
            }

            if (!hitObjects.Contains(collision.gameObject.GetInstanceID()))
            {
                Vector3 vector_EffectToEnemy = collision.gameObject.transform.position - transform.position;
                Vector3 vector_projected = Vector3.Project(vector_EffectToEnemy, transform.right);
                float mag = vector_projected.magnitude;
                Vector3 vector_force = vector_projected + mag * transform.up;
                vector_force.Normalize();
                collision.gameObject.GetComponent<EnemyController>().EnemyHit(vector_force, mag, hitPower);
                //collision.gameObject.GetComponent<EnemyController>().EnemyHit((Vector2)transform.position - collision.)
                hitObjects.Add(collision.gameObject.GetInstanceID());

            }
        }
    }
}
