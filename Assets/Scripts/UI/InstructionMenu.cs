using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
