using UnityEngine;
using System.Collections;

public class EnemyDemo : MonoBehaviour {
    public float SkillCooldownTimer = 8;
    public float SkillCooldownTime = 8;
    public float skillcastTime = 0;
    bool isCastSkill =false;
    public GameObject skillEffect;
    public GameObject skillEffectPrefab;
    GameObject player;

    Vector3 movingDir;
	// Use this for initialization
	void Start () {
        SkillCooldownTimer = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (isCastSkill)
        {
            skillcastTime -= Time.deltaTime;
            transform.position += movingDir*Time.deltaTime*3.0f;
            if (skillcastTime < 0)
            {
                skillEffect.GetComponent<EnemyImpactEffect>().Destroy();
                GetComponent<EnemyController>().EndSkill();
                isCastSkill = false;

            }
        }
        else
        {
            SkillCooldownTimer -= Time.deltaTime;
        }
        
        //ready for cast skill
        if (SkillCooldownTimer < 0)
        {
            if (GetComponent<EnemyController>().CastSkill())
            {
                movingDir = new Vector3((player.transform.position - transform.position).x, 0,0).normalized;
                //skill start to cool down
                SkillCooldownTimer= SkillCooldownTime;
                skillEffect = GameObject.Instantiate(skillEffectPrefab, transform) as GameObject;
                skillEffect.transform.position = transform.position;
                skillcastTime = 5;
                isCastSkill = true;

            }
        }
        
	    
	}
}
