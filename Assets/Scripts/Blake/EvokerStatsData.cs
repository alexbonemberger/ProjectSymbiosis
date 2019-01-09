using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EvokerStatsData : MonoBehaviour {

    public int level;
    public int skillPoints;
    public int exp;
    public float gold;

    public EvokerStatsData(EvokerStats evokerStats)
    {
        level = evokerStats.level;
        skillPoints = evokerStats.skillPoints;
        exp = evokerStats.exp;
        gold = evokerStats.gold;
    }
}