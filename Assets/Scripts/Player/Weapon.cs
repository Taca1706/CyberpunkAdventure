using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Shoot();
            audioManager.PlaySFX(audioManager.fire);
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
