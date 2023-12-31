using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private GameObject enemies;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y + 1f) * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(enemies)
            {
                collision.gameObject.GetComponent<PlayerHealth>().health -= enemies.GetComponent<Enemies>().damage;
            }
        }
        if(!collision.gameObject.CompareTag("Enemies") && !collision.gameObject.CompareTag("Item"))
        {
            Destroy(gameObject);
        }
    }
}
