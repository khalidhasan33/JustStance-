using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float offset;
    public Transform shotPoint;
    public GameObject projectile;
    public bool onEnemyHand;
    public float bulletForce;
    public float startTimeBtwShots;
    public bool activeShootFromEnemy;
    private bool facingLeft = true;
    private float timeBtwShots;
    private Transform player;
    private Enemy enemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if(GameObject.Find("Weapon(onHand)") != null && !onEnemyHand)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Rotate(difference);
            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                    timeBtwShots = startTimeBtwShots;
                }
            }
            else {
                timeBtwShots -= Time.deltaTime;
            }
        }
        if(onEnemyHand)
        {
            if(activeShootFromEnemy)
            {
                Vector3 difference = player.position - transform.position;
                Rotate(difference);
            }
            if (timeBtwShots <= 0 && activeShootFromEnemy)
            {
                Shoot();
                timeBtwShots = startTimeBtwShots;
            }
            else {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        float recoil = Random.Range(-7, 7);
        shotPoint.Rotate(0, 0, recoil,  Space.Self);
        GameObject bullet = Instantiate(projectile, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.up * bulletForce, ForceMode2D.Impulse);
        shotPoint.Rotate(0, 0, -recoil,  Space.Self);
    }

    void Rotate(Vector3 difference)
    {
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        Vector3 localScale = transform.localScale;
        if (difference.x < -0.1)
        {
            facingLeft = true;
        }
        else if (difference.x > 0.1)
        {
            facingLeft = false;
        }
        if (((facingLeft ) && (localScale.y > 0 )) || ((!facingLeft) && (localScale.y < 0 ))) 
        {
            localScale.y *= -1;
            shotPoint.Rotate(0, 180, 180,  Space.Self);
        }
        transform.localScale = localScale;
    }
}
