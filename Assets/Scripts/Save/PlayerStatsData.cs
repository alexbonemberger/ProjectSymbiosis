using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatsData {

    public int level;
    public float gold;
    public float exp;

    public PlayerStatsData(PlayerStats playerStats)
    {
        level = playerStats.level;
        gold = playerStats.gold;
        exp = playerStats.exp;
    }

}
