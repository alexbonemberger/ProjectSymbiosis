using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    private Clock clock;
    private List<Clock> clocks;

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
                print(clocks[i].hasEnded);
                clocks[i].hasEnded = true;
                print("teste");
                print(clocks[i].hasEnded);
                clocks.RemoveAt(i); //remove após executar o código
            }
        }
    }

    private class Clock
    {
        public string name;
        public double startTime;
        public bool hasEnded;
    }

    public void AddClock(string name, double startTime)
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
