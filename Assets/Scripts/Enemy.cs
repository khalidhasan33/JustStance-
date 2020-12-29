using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public float stoppingDistance;
	public float retreatDistance;
	public Weapon weapon;
	private Transform player;
	private bool move;
	private Animator anim;

	void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
	}

	void Update()
	{
		if(move)
		{
			if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
			{
				transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
				anim.SetBool("isRunning", true);
			} 
			else if(Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
			{
				transform.position = this.transform.position;
				anim.SetBool("isRunning", false);
			}
			else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
			{
				transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
				anim.SetBool("isRunning", true);
			}
			weapon.activeShootFromEnemy = true;
			anim.SetFloat("Horizontal", player.position.x - transform.position.x);
		}
		else
		{
			weapon.activeShootFromEnemy = false;
	        anim.SetBool("isRunning", false);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Player")
    	{
    		move = true;
    	}
    }

    void OnTriggerStay2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Player")
    	{
    		move = true;
    	}
    }

    void OnTriggerExit2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Player")
    	{
	        move = false;
    	}
    }
}