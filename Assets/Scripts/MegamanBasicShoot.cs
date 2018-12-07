using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanBasicShoot : MonoBehaviour {

    public static int Count = 0;
    public static float lastShootTime = 0;
    public static int overHeatCount = 0;
    public static bool canShoot = true;

    // Use this for initialization
    void Awake () {
        Count++;
        if (Time.time - lastShootTime < 1f)
        {
            overHeatCount++;
            if (overHeatCount >= 3)
            {
                canShoot = false;
                overHeatCount = 0; //reseta o overheat mas impede de atirar ate passar o tempo de overheat
                Timer.AddClock("CanShootAgain", 2f);
            }
        }
        else overHeatCount--;
        lastShootTime = Time.time;
    }

    private void Update()
    {
        //if (canShoot == false) Timer.AddClock("CanShootAgain", 2f);
    }

    private void ReverseOverHeat()
    {
        if(Time.time - lastShootTime > 1f)
        {
            canShoot = true;
            overHeatCount = 0;
        }
    }

    private void OnDestroy()
    {
        Count--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            //print(collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
