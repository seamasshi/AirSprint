using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public float EnemyCoreRadius = 0.4f;
    public float enemyCoreHitPowerFactor = 3.0f;
    public float enemyHitPowerFactor = 1.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        


    }

    private void FixedUpdate()
    {
        float x = GetComponent<Rigidbody2D>().velocity.x;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x*0.92f, GetComponent<Rigidbody2D>().velocity.y);
    }
    /// <summary>
    /// deal with the effect of enemy when hit
    /// </summary>
    /// <param name="dir"> normalized direction of the hit force</param>
    /// <param name="distance"> distance from the hit to enemy's center</param>
    /// <param name="power">the attact power of the hit</param>
    public void EnemyHit(Vector2 dir,float distance ,float power)
    {        
        //first stop the enemy from falling
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //add a force to enemy when hit - according to the direction of this hit
        GetComponent<Rigidbody2D>().AddForce(dir*power*enemyHitPowerFactor);

        //when hit the core, add another blow up force
        if (distance <= EnemyCoreRadius)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * power *enemyCoreHitPowerFactor);
        }
        
        
    }
}
