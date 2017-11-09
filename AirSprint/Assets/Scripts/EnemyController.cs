using UnityEngine;
using System.Collections;
public enum EnemyState
{
    OnGround,
    InAirNormal,
    CastSkill
};
public class EnemyController : MonoBehaviour {
    public EnemyState enemyState = EnemyState.InAirNormal;


    public float EnemyCoreRadius = 0.15f;
    public float enemyCoreHitPowerFactor = 3.0f;
    public float enemyHitPowerFactor = 1.5f;
    public float groundLine = -3.5f;
    public float HP = 10000;
    public float MaxHP = 10000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        


    }

    private void FixedUpdate()
    {
        //damp on X axis
        float x = GetComponent<Rigidbody2D>().velocity.x;
        GetComponent<Rigidbody2D>().velocity = new Vector2(x*0.95f, GetComponent<Rigidbody2D>().velocity.y);
        switch (enemyState)
        {
            case EnemyState.CastSkill:
                break;
            case EnemyState.InAirNormal:
                if (!CheckAboveGround())
                {
                    enemyState = EnemyState.OnGround;
                }
                break;
            case EnemyState.OnGround:
                break;
                
        }
        
    }
    /// <summary>
    /// deal with the effect of enemy when hit
    /// </summary>
    /// <param name="dir"> normalized direction of the hit force</param>
    /// <param name="distance"> distance from the hit to enemy's center</param>
    /// <param name="power">the attact power of the hit</param>
    public void EnemyHit(Vector2 dir,float distance ,float power)
    {
        //-hp
        HP -= power;
        if (HP < 0)
        {
            HP = MaxHP;
        }
        if (enemyState != EnemyState.CastSkill)
        {           
            enemyState = EnemyState.InAirNormal;            
            //first stop the enemy from falling
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //add a force to enemy when hit - according to the direction of this hit
            GetComponent<Rigidbody2D>().AddForce(dir * power * enemyHitPowerFactor);
            //Debug.Log("Hit" + distance.ToString());
            //when hit the core, add another blow up force
            if (distance <= EnemyCoreRadius)
            {
               // Debug.Log("Hit distance:" + distance.ToString());
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * power * enemyCoreHitPowerFactor);
            }
        }
        
        
        
    }
    /// <summary>
    /// Only tell if the object is above ground line
    /// </summary>
    /// <returns>true if above ground line</returns>
    public bool CheckAboveGround()
    {
        if (transform.position.y < groundLine) // land on the ground
        {            
            return false;
        }
        return true;
    }

    public bool CastSkill()
    {
        if (enemyState == EnemyState.OnGround)
        {
            enemyState = EnemyState.CastSkill;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool EndSkill()
    {
        if (enemyState == EnemyState.CastSkill)
        {
            enemyState = EnemyState.InAirNormal;
            return true;
        }
        else
        {
            return false;
        }
    }
    public float getHPPercentage()
    {
        return HP / MaxHP;

    }

}
