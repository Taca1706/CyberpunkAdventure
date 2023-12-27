using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionActive : MonoBehaviour
{
    public List<GameObject> Instructions = new List<GameObject>();
    public int index = 0;

    public void Show()
    {
        Instructions[0].SetActive(true);
        Time.timeScale = 0f;
    }
    public void Close()
    {
        Instructions[index].SetActive(false);
        Time.timeScale = 1f;
    }
    public void Next()
    {
        Instructions[index].SetActive(false);
        index++;
        if (index < Instructions.Count)
        {
            Instructions[index].SetActive(true);
        }
        else
        {
            Instructions[index-1].SetActive(false);
            index = 0;
            Instructions[index].SetActive(true);
        }
    }
    public void Previous()
    {
        Instructions[index].SetActive(false);
        index--;
        if (index >= 0)
        {
            Instructions[index].SetActive(true);
        }
        else
        {
            Instructions[index+1].SetActive(false);
            index = Instructions.Count - 1;
            Instructions[index].SetActive(true);
        }
    }
}
