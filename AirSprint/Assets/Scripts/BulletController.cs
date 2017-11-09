using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public Vector2 velocity;
    public float life = 5;
    public float hitPower = 12;
	// Use this for initialization
	void Start () {
	    //velocity = new Vector2(0, 0);

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(0,0, Time.fixedDeltaTime*2000f));
        life -= Time.fixedDeltaTime;
        if (life < 0)
        {
            GameObject.Destroy(gameObject);
        }
	}

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().EnemyHit(Vector3.zero,0,hitPower);
            GameObject.Destroy(gameObject);
        }
    }
    
    public void SetVelocity(Vector2 vec)
    {
        velocity = vec;
    }
}
