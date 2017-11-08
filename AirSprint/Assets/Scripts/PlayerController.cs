using UnityEngine;
using System.Collections;

public enum PlayerState {
    OnGround,
    InAirNormal,
    Hover
};

public class PlayerController : MonoBehaviour {



    public PlayerState playerState;


    public float playerEnergy;
    public float playerEnergyRecoverRate = 5.0f;
    public float playerEnergyMax = 100;
     
    public GameObject dashEffectPrefab;
    public GameObject dashEffect;


    public float hoverTimer;
    public float hoverTime = 0.2f;
    public float globalCooldownTimer;
    public float globalCooldownTime = 0.25f;

    public float groundLine = -3.5f;

    void Start () {
        InputManager.inputDelegate_Sweep += OnSweep;

        playerEnergy = playerEnergyMax;
        playerState = PlayerState.InAirNormal;
        hoverTimer = 0;
        globalCooldownTimer = 0;
    }
	
	
	void OnDisable () {
        InputManager.inputDelegate_Sweep -= OnSweep;

    }


    private void FixedUpdate()
    {
        
        globalCooldownTimer -= Time.fixedDeltaTime;

        


        CheckAboveGround();


        switch (playerState)
        {

            case PlayerState.InAirNormal:
                break;
            case PlayerState.Hover:

                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                if (hoverTimer > 0)
                {
                    hoverTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    playerState = PlayerState.InAirNormal;
                }
                break;
            case PlayerState.OnGround:
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                break;
        }


        if (playerEnergy < 0)
            playerEnergy = 0;
        playerEnergy += playerEnergyRecoverRate * Time.fixedDeltaTime;
        if (playerEnergy > playerEnergyMax)
            playerEnergy = playerEnergyMax;
    }
    /// <summary>
    /// check if the character is above groundline - if not, correct it
    /// </summary>
    /// <returns> return true when above ground</returns>
    public bool CheckAboveGround()
    {
        if (transform.position.y < groundLine) // land on the ground
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            transform.position = new Vector3(transform.position.x, groundLine, transform.position.z);
            playerState = PlayerState.OnGround;
            return false;
        }        
        return true;
    }

    public bool CheckInAir()
    {
        if (transform.position.y > groundLine) 
        {            
            
            return true;
        }
        return false;
    }

    /// <summary>
    /// reaction of the character on "sweep" action
    /// </summary>
    /// <param name="vec"> normalize direction of the sweep action</param>
    /// <param name="mag"> length of sweep action</param>
    public void OnSweep(Vector2 vec, float mag)
    {
        //Debug.Log("On Sweep");
        if(ActivateSkill())
            Dash(vec, 3.0f);
    }

    /// <summary>
    /// actions for every skill casted
    /// </summary>
    /// <returns> if the character colddown & energy is ready for another skill</returns>
    public bool ActivateSkill()
    {
        if (playerEnergy >= 30)
        {
            playerEnergy -= 30;
            if (globalCooldownTimer > 0)
            {
                return false;
            }
            else
            {
                //first stop the character from falling
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                //make character into colddown 
                globalCooldownTimer = globalCooldownTime;

                return true;


            }
        }
        return false;
    }

    /// <summary>
    /// a skill- will move the character and instantiate a dash effect which will collide with enemies 
    /// </summary>
    /// <param name="vec">normalized direction</param>
    /// <param name="distance"> length </param>
    void Dash(Vector2 vec, float distance)
    {
        
            Vector3 origin = transform.position;
            transform.position += (Vector3)vec * distance;
            Vector2 newVec = vec;
            if (!CheckAboveGround())
            {


                newVec.y = 0;
                newVec.Normalize();
                transform.position = origin + (Vector3)newVec * distance;

            }


            dashEffect = Instantiate(dashEffectPrefab, (origin + transform.position) / 2.0f, Quaternion.LookRotation(Vector3.forward, (Vector3)newVec)) as GameObject;
            if (CheckInAir())
            {
                playerState = PlayerState.Hover;
                hoverTimer = hoverTime;

            }
        
            
    }
    /// <summary>
    /// effect to the player when skills hit - energy backflow
    /// </summary>
    /// <param name="energyBackflowValue"> add energy to player when the skill hit</param>
    public void SkillHit(float energyBackflowValue)
    {
        playerEnergy += energyBackflowValue;
        if (playerEnergy > playerEnergyMax)
            playerEnergy = playerEnergyMax;
    }
    
    void CreateDashEffect(Vector2 pos)
    {


    }

    public float getPlayerEnergyPercentage()
    {
        return playerEnergy / playerEnergyMax;

    }
}
