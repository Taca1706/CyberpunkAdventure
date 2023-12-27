using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenIns : MonoBehaviour
{
    [SerializeField] GameObject InsMenu_1;

    public void Close()
    {
        InsMenu_1.SetActive(false);
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            InsMenu_1.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
