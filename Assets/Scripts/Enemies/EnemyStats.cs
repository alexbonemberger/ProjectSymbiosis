using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Create Enemy Stats")]
public class EnemyStats : ScriptableObject {

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

    public float penetration;

    public float gold;
    public float exp;
}
