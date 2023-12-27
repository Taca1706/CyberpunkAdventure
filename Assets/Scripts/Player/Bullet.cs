using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private GameObject player;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemies enemy = collision.GetComponent<Enemies>();
        if (enemy != null)
        {
            enemy.TakeDamage(playerHealth.damage);
        }
        if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Item"))
        {
            Destroy(gameObject);
        }
        
    }
}
