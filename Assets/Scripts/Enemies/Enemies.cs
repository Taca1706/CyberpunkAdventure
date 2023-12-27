using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField]
    private Image healthBar;
    private Animator anim;
    public float damage;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    private GameObject player;
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
        maxHealth = health;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health > 0)
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            Movement();
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 10f)
        {
            timer += Time.deltaTime;
            if (timer > 1.4)
            {
                timer = 0;
                shoot();
            }
        }
    }

    private void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            anim.SetBool("isDead", true);
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(DestroyEnemies());
    }

    IEnumerator DestroyEnemies()
    {
        yield return new WaitForSeconds(0.4f);

        Destroy(gameObject);
    }

    private void Movement()
    {
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            Flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }
            else
            {
                currentPoint = pointB.transform;
            }
            Flip();
        }
    }
}
