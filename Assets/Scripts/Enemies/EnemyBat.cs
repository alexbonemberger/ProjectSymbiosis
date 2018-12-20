using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour {

    private Rigidbody2D rb2D;
    private bool isPatrolling;
    private Vector3 patrolEdges; //x is left, y is right and z is top
    private float speed;
	// Use this for initialization
	void Awake () {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        patrolEdges = new Vector3(-10f, 10f, 10f); //Random.Range(800f,1000f)
        isPatrolling = true;
        speed = 2f;
	}
	
	// Update is called once per frame
	void Update () {
        //print(rb2D.velocity);
		if (isPatrolling)
        {
            //get back to patrolling high
            if (transform.position.y < patrolEdges.z)
            {
                if (rb2D.velocity.y < speed)
                {
                    rb2D.velocity = new Vector2(0, speed); // move up
                }
            }
            else
            {
                if (transform.position.x >= patrolEdges.y)
                {
                    rb2D.velocity = new Vector2(-speed, 0); // move left
                }

                if (transform.position.x <= patrolEdges.x || rb2D.velocity.y == speed)
                {
                    rb2D.velocity = new Vector2(speed, 0); // move right
                }
            }
        }
	}
}
