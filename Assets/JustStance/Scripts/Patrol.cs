using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

	public float speed;
	private float waitTime;
	public float startWaitTime;

	public Transform[] moveSpots;
	private int randomSpot;
	private int randomSpotTemp;
	private bool move;

	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
		waitTime = startWaitTime;
		randomSpotTemp = 0;
		randomSpot = randomSpotTemp;
		move = true;
	}

	void Update()
	{
		if(move)
		{
			transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

			if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
			{
				if(waitTime <= 0)
				{
					if(moveSpots.Length != 0)
					{
						RandomSpot();
					}
					waitTime = startWaitTime;
					anim.SetBool("isRunning", true);
					anim.SetFloat("Horizontal", moveSpots[randomSpot].position.x - transform.position.x);
				}else
				{
					waitTime -= Time.deltaTime;
					anim.SetBool("isRunning", false);
				}
			}
		}
	}

	void RandomSpot()
	{
		randomSpotTemp = Random.Range(0, moveSpots.Length);
		while(randomSpot == randomSpotTemp)
		{
			randomSpotTemp = Random.Range(0, moveSpots.Length);
		}
		randomSpot = randomSpotTemp;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Player")
    	{
	        move = false;
	        anim.SetBool("isRunning", false);
    	}
    }
    void OnTriggerExit2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Player")
    	{
	        move = true;
	        anim.SetBool("isRunning", true);
	    }
    }
}
