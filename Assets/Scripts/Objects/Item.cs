using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item : MonoBehaviour
{
    public int healthRecover;
    public int damageIncrease;
    private GameObject player;
    public AudioClip collectSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            player.GetComponent<PlayerHealth>().AddHealth(healthRecover);
            player.GetComponent<PlayerHealth>().AddDamage(damageIncrease);
            Destroy(gameObject);
        }
    }
}
