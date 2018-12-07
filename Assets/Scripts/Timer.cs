using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    private static Clock clock;
    private static List<Clock> clocks;

    // Use this for initialization
    void Awake() {
        clocks = new List<Clock>();
        //Time.timeScale = 0.25f;
    }

    void Update() {
        for (int i = 0; i < clocks.Count; i++)
        {
            clocks[i].startTime -= Time.deltaTime;
            
            if (clocks[i].startTime <= 0)
            {
                //print(clocks[i].hasEnded);
                clocks[i].hasEnded = true;
                
                if(clocks[i].hasEnded)
                {
                    if (clocks[i].name == "CanShootAgain")
                    {
                        MegamanBasicShoot.canShoot = true;
                        //print("canShootAgain");
                    }
                }

                clocks.RemoveAt(i); //remove após executar o código
            }
        }
    }

    private class Clock
    {
        public string name;
        public float startTime;
        public bool hasEnded;
    }

    public static void AddClock(string name, float startTime)
    {
        clock = new Clock
        {
            name = name,
            startTime = startTime,
            hasEnded = false
        };
        clocks.Add(clock);
    }

    public void RemoveClock(string name)
    {
        clocks.RemoveAll(L => L.name == name);
    }

}
