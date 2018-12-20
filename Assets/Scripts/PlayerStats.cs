using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private int level;
    private int progressionLevel;
    private float progression;
    public float health;
    public float mana;
    public float movementSpeed;
    public float range;
    public float attackSpeed;
    public float damagePower;
    public float physicalPower;
    public float magicalPower;
    public float physicalProtection;
    public float magicalProtection;
    public float regenHP;
    public float regenMP;

    //itens
    public float criticalRate;
    public float criticalPower;
    public float physicalProtectionIgnoreAmount;
    public float physicalProtectionIgnorePercent;

    //level up and collectables
    public float gold;
    public float exp;

    //battle
    private float healthCurrent;

    // Use this for initialization
    void Awake () {
        level = 0;
        gold = 0;
        exp = 0;
        progressionLevel = 0;
        progression = Mathf.Pow(2f, (level / 100)) * (level / 100f - level / 100 + 1); //each rank doubles the power of last rank and add 1 percent per level in the rank
        health = progression * (470f + progressionLevel * 75f);
        healthCurrent = health;
        mana = progression * (245f + progressionLevel * 35f);
        movementSpeed = 375f;
        range = 12f;
        attackSpeed = 1.02f * (progressionLevel * 0.019f + 1);
        
        physicalProtection = progression * (14f + progressionLevel * 2.9f);
        magicalProtection = progression * (31f + progressionLevel * 0.9f);
        regenHP = progression * (8.7f + progressionLevel * 0.7f);
        regenMP = progression * (4.45f + progressionLevel * 0.35f);

        //itens
        physicalPower = 0f;
        magicalPower = 0f;
        criticalRate = 0f;
        criticalPower = 100f;
        physicalProtectionIgnoreAmount = 0f;
        physicalProtectionIgnorePercent = 0f;

        //axb itens
        damagePower = (40f + progressionLevel * 2.4f) * progression + physicalPower * progression;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreaseGold(float gold)
    {
        this.gold += gold;
    }

    public void IncreaseExp(float exp)
    {
        this.exp += exp;
    }
}
