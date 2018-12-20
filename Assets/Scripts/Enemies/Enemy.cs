using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private PlayerStats playerStats;
    [SerializeField]
    private int tier;
    [SerializeField]
    private int level;
    [SerializeField]
    private EnemyStats enemyStats;
    private float progression;

    //status from EnemyStats
    public string enemyName;
    public float health;
    public float mana;
    public float movementSpeed;     //default movementSpeed = 375f;
    public float range;             //default range = 12f;
    public float attackSpeed;       //attackSpeed = 1.38f;
    public float damagePower;

    public float physicalProtection;
    public float magicalProtection;
    public float regenHP;
    public float regenMP;


    //itens
    private float physicalPower;
    private float magicalPower;
    private float criticalRate;
    private float criticalPower;
    private float penetration;
    private float penetrationPercent;
    private float physicalProtectionIgnoreAmount;
    private float physicalProtectionIgnorePercent;
    private float magicalProtectionIgnoreAmount;
    private float magicalProtectionIgnorePercent;

    //battle
    private float healthCurrent;
    private float gold;
    private float exp;

    // Use this for initialization
    void Awake () {
        playerStats = GameObject.FindGameObjectWithTag("MasterGameObject").GetComponent<PlayerStats>();
        progression = Mathf.Pow(2f, (level / 100)) * (level / 100f - level / 100 + 1); //each rank doubles the power of last rank and add 1 percent per level in the rank
        print(progression);

        //status from EnemyStats
        //load
        enemyName          = enemyStats.enemyName;
        health             = enemyStats.health * progression;
        healthCurrent      = health;
        mana               = enemyStats.mana * progression;
        movementSpeed      = enemyStats.movementSpeed;         //default movementSpeed  = 375f;
        range              = enemyStats.range;                 //default range          = 12f;
        attackSpeed        = enemyStats.attackSpeed;           //default attackSpeed    = 1.38f;

        physicalProtection = enemyStats.physicalProtection * progression;
        magicalProtection  = enemyStats.magicalProtection * progression;
        regenHP            = enemyStats.regenHP * progression;
        regenMP            = enemyStats.regenMP * progression;
        penetration        = enemyStats.penetration * progression;
        
        //itens
        physicalPower = 0f;
        magicalPower = 0f;
        criticalRate = 0f;
        criticalPower = 100f;
        penetrationPercent = 0f; // ruina do tita
        physicalProtectionIgnoreAmount = 0f;
        physicalProtectionIgnorePercent = 0f;
        magicalProtectionIgnoreAmount = 0f;
        magicalProtectionIgnorePercent = 0f;

        //last calculation
        //axb itens
        damagePower = enemyStats.damagePower * progression + physicalPower * progression;
        gold = enemyStats.gold / 1298f * progression; //1298f exp to lv 20
        exp = enemyStats.exp / 1298f * progression; //1298f exp to lv 20
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void TakeDamage(float damage)
    {
        print("damaged");
        print(healthCurrent);
        print(damagePower);
        damage = (damage * 100f / ((100f + physicalProtection) * (1f - physicalProtectionIgnorePercent) - physicalProtectionIgnoreAmount)) * (1 - penetrationPercent) - penetration; // smite formula
        healthCurrent -= damage;
        if (healthCurrent <= 0)
            Death();
    }

    public void Death()
    {
        playerStats.IncreaseGold(gold);
        playerStats.IncreaseExp(exp);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            TakeDamage(playerStats.damagePower);
            Destroy(collision.gameObject);
        }
    }
}
